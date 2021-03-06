﻿using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System;
using System.IO;
using AssemblyCSharp;

public class SocketEngine
{
    #region 变量定义

    TcpClient tcpclient = new TcpClient();
    IPAddress ip; //主机ip
    int port;
    readonly byte[] recvByte = new byte[20480]; //接收的数据，必须为字节
    int wRealySize; //接收的数据长度
    Thread connectThread; //连接线程
    public bool isConnected;
    ISocketEvent socketEvent;

    private static SocketEngine _instance;

    //网络流
    NetworkStream stream;
    bool isWait;
    byte[] sources;
    int waitLen;
    public static bool hasStartTimer;
    private int disConnectCount;
    System.Timers.Timer t;

    #endregion

    #region 连接Socket

    public static SocketEngine Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SocketEngine();
            }

            return _instance;
        }
    }

    //设置数据接口
    public void SetSocketEvent(ISocketEvent _event)
    {
        socketEvent = null;
        socketEvent = _event;
    }

    //初始化
    public void InitSocket(string idStr, int sPort)
    {
        UIManager.instance.Show(UIType.UILoading);
        //定义服务器的IP和端口，端口与服务器对应 杰哥--192.168.0.203//远程112.74.208.63
        if (isConnected)
        {
            socketEvent.OnEventTCPSocketLink();
            return;
        }

        ip = IPAddress.Parse(idStr); //可以是局域网或互联网ip，
        port = sPort;
        var connectThread = new Thread(new ThreadStart(SocketConnet));
        connectThread.Start();
    }

    //连接Socket
    void SocketConnet()
    {
        try
        {
            if (tcpclient == null)
                tcpclient = new TcpClient(APIS.tcpFamily);
            //防止延迟,即时发送!
            tcpclient.NoDelay = true;
            SocketEventHandle.Instance.iscloseLoading = false;
            tcpclient.BeginConnect(ip, port, new AsyncCallback(SocketConnectCallBack), tcpclient);
        }
        catch (Exception)
        {
            SocketQuit();
        }
    }

    #endregion

    #region 接收数据

    int testIndex;

    //连接回调
    void SocketConnectCallBack(IAsyncResult ar)
    {
        if ((tcpclient != null) && (tcpclient.Connected))
        {
            stream = tcpclient.GetStream();
            Asyncread(tcpclient);
            isConnected = true;
            MyDebug.Log("服务器已经连接!");
            socketEvent.OnEventTCPSocketLink();
        }

        var t = (TcpClient) ar.AsyncState;
        try
        {
            t.EndConnect(ar);
        }
        catch (Exception ex)
        {
            //设置标志,连接服务端失败!
            MyDebug.Log(ex.ToString());
            SocketQuit();
        }
    }

    /// <summary>
    /// TCP读数据的回调函数
    /// </summary>
    /// <param name="ar"></param>
    private void TCPReadCallBack(IAsyncResult ar)
    {
        MyDebug.Log("TCPReadCallBack");
        var state = (StateObject) ar.AsyncState;
        ////主动断开时
        MyDebug.Log(!state.client.Connected);
        if ((state.client == null) || (!state.client.Connected))
        {
            SocketQuit();
            return;
        }

        int numberOfBytesRead;
        var mas = state.client.GetStream();
        numberOfBytesRead = mas.EndRead(ar);
        state.totalBytesRead += numberOfBytesRead;
        if (numberOfBytesRead > 0)
        {
            var dd = new byte[numberOfBytesRead];
            Array.Copy(state.buffer, 0, dd, 0, numberOfBytesRead);
            if (isWait)
            {
                var temp = new byte[sources.Length + dd.Length];
                sources.CopyTo(temp, 0);
                dd.CopyTo(temp, sources.Length);
                sources = temp;
                if (sources.Length >= waitLen)
                {
                    ReceiveCallBack(sources.Clone() as byte[]);
                    isWait = false;
                    waitLen = 0;
                }
            }
            else
            {
                sources = null;
                ReceiveCallBack(dd);
            }

            mas.BeginRead(state.buffer, 0, StateObject.BufferSize,
                new AsyncCallback(TCPReadCallBack), state);
            MyDebug.Log("注册接收回调");
        }
        else
        {
            //被动断开时
            mas.Close();
            state.client.Close();
            mas = null;
            state = null;
            //设置标志,连接服务端失败!
            SocketEventHandle.Instance.iscloseLoading = true;
            MyDebug.Log("客户端被动断开");
            SocketQuit();
            if (socketEvent != null)
            {
                socketEvent.ISocketEngineSink();
            }
        }
    }

    private void ReceiveCallBack(byte[] m_receiveBuffer)
    {
        //通知调用端接收完毕
        try
        {
            var ms = new MemoryStream(m_receiveBuffer);
            var buffers = new BinaryReader(ms, Encoding.Default);
            ReadBuffer(buffers);
        }
        catch (Exception ex)
        {
            MyDebug.LogError("123");
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// 异步读TCP数据
    /// </summary>
    /// <param name="sock"></param>
    private void Asyncread(TcpClient sock)
    {
        var state = new StateObject
        {
            client = sock
        };
        NetworkStream stream;
        try
        {
            stream = sock.GetStream();
            if (stream.CanRead)
            {
                try
                {
                    var ar = stream.BeginRead(state.buffer, 0, StateObject.BufferSize,
                        new AsyncCallback(TCPReadCallBack), state);
                }
                catch (Exception ex)
                {
                    //设置标志,连接服务端失败!
                    MyDebug.Log(ex.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            //设置标志,连接服务端失败!
            MyDebug.Log(ex.ToString());
        }
    }

    private void ReadBuffer(BinaryReader buffers)
    {
        var infoBytes = buffers.ReadBytes(4);
        var tInfo = new TCP_Info();
        tInfo = NetUtil.BytesToStruct<TCP_Info>(infoBytes);
        int lens = tInfo.wPacketSize;
        disConnectCount = 0;
        if (!hasStartTimer && lens == 16)
        {
            StartTimer();
            hasStartTimer = true;
        }

        if (lens > buffers.BaseStream.Length)
        {
            waitLen = lens;
            isWait = true;
            buffers.BaseStream.Position = 0;
            var dd = new byte[buffers.BaseStream.Length];
            var temp = buffers.ReadBytes((int) buffers.BaseStream.Length);
            Array.Copy(temp, 0, dd, 0, (int) buffers.BaseStream.Length);
            if (sources == null)
            {
                sources = dd;
            }

            return;
        }

        MyDebug.Log("tInfo.wPacketSize:" + tInfo.wPacketSize);
        var buffer = buffers.ReadBytes(tInfo.wPacketSize - 4);
        MyDebug.Log("ArrayCopy Over!!!!");
        HandReceiveData(tInfo, buffer);
        if (buffers.BaseStream.Position < buffers.BaseStream.Length)
        {
            ReadBuffer(buffers);
        }

        MyDebug.Log("readBuffer Over!!!!");
    }

    void HandReceiveData(TCP_Info tInfo, byte[] recvByte)
    {
        wRealySize = recvByte.Length;
        MyDebug.Log("解密前wRealySize:" + wRealySize);
        if (wRealySize == 0)
        {
            SocketEventHandle.Instance.iscloseLoading = true;
            SocketConnet();
            return;
        }

        var tmpBuf = new byte[wRealySize + 3];
        Array.Copy(recvByte, tmpBuf, wRealySize);
        wRealySize = NetUtil.CrevasseBuffer(tInfo, tmpBuf, wRealySize);
        MyDebug.Log("解密后wRealySize:" + wRealySize);
        if (wRealySize < 4)
        {
            SocketEventHandle.Instance.iscloseLoading = true;
            SocketQuit();
            return;
        }

        //解释数据
        var wDataSize = wRealySize - 4;
        var command = (TCP_Command) NetUtil.BytesToStruct(tmpBuf, typeof(TCP_Command), 4);
        var buff = new byte[wDataSize];
        Array.Copy(tmpBuf, 4, buff, 0, wDataSize);
        MyDebug.Log("HandReceiveData 11:");
        if (command.wMainCmdID == (int) CMD_TYPE.MDM_KN_COMMAND &&
            command.wSubCmdID == (int) CMD_TYPE.SUB_KN_DETECT_SOCKET)
        {
        }
        else
        {
            MyDebug.SocketLog("REV- --main command: " + command.wMainCmdID + "-- Sub Command:" + command.wSubCmdID);
            //处理消息
            socketEvent.OnEventTCPSocketRead(command.wMainCmdID, command.wSubCmdID, buff, wDataSize);
        }

        MyDebug.Log("HandReceiveData Over:");
    }

    #endregion

    #region 发送数据

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <param name="buffer"></param>
    /// <param name="wSize"></param>
    /// <param name="callback"></param>
    /// <param name="param"></param>
    public void SendScoketData(int main, int sub, byte[] buffer = null, int wSize = 0,
        Action<Hashtable> callback = null, Hashtable param = null)
    {
        if (NetUtil.MDM_KN_COMMAND != main && NetUtil.SUB_KN_DETECT_SOCKET != sub)
            MyDebug.SocketLog("Send-------------------------------------------------------------------- --main: " +
                              main + "-- Sub:" + sub);
        if (null == buffer) wSize = 0;
        MyDebug.Log("wSize:" + wSize);
        var headSize = Marshal.SizeOf(typeof(TCP_Head));
        var SendSize = wSize + headSize;
        var head = new TCP_Head();
        head.CommandInfo.wMainCmdID = (ushort) main;
        head.CommandInfo.wSubCmdID = (ushort) sub;
        var SendBuffer = new byte[NetUtil.SOCKET_TCP_BUFFER];
        var Headbuffer = NetUtil.StructToBytes(head);
        MyDebug.Log("Headbuffer.Length:" + Headbuffer.Length);
        MyDebug.Log("SendBuffer.Length:" + SendBuffer.Length);
        MyDebug.Log("headSize:" + headSize);
        Array.Copy(Headbuffer, SendBuffer, headSize);
        if (null != buffer)
            Array.Copy(buffer, 0, SendBuffer, headSize, wSize);
        //加密
        SendSize = NetUtil.EncryptBuffer(SendBuffer, SendSize);
        SendMessage(SendBuffer, SendSize);
    }

    public bool SendMessage(byte[] data, int size)
    {
        try
        {
            if (stream != null && tcpclient.Connected)
            {
                MyDebug.Log("SendSucess!!!");
                stream.Write(data, 0, size);
                return true;
            }
            else
            {
                SocketEventHandle.Instance.iscloseLoading = true;
                MyDebug.Log("1服务器已断开连接，请重新登录!服务器连接状态：" + tcpclient.Connected);
                SocketQuit();
                return false;
            }
        }
        catch (Exception)
        {
            SocketEventHandle.Instance.iscloseLoading = true;
            MyDebug.Log("2服务器已断开连接，请重新登录");
            SocketQuit();
            return false;
        }
    }

    #endregion

    private void StartTimer()
    {
        if (t == null)
        {
            t = new System.Timers.Timer(20000); //实例化Timer类，设置间隔时间为10000毫秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler(Timeout); //到达时间的时候执行事件；
            t.AutoReset = true; //设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true; //是否执行System.Timers.timer.Elapsed事件；
        }
        else
        {
            t.Start();
        }
    }

    public void Timeout(object source, System.Timers.ElapsedEventArgs e)
    {
        disConnectCount += 1;
        if (disConnectCount >= 15)
        {
            t.Stop();
            disConnectCount = 0;
            SocketQuit();
            SocketEventHandle.Instance.iscloseLoading = true;
            MyDebug.Log("3服务器已断开连接，请重新登录");
            return;
        }
    }

    public void SocketQuit()
    {
        MyDebug.Log("SocketQuit");
        //关闭线程
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }

        //最后关闭服务器
        if (tcpclient != null)
        {
            tcpclient.Close();
            tcpclient = null;
        }

        stream = null;
        NetUtil.Init();
        isConnected = false;
    }
}