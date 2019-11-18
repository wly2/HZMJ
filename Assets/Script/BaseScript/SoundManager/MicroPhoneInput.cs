using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections;
using AssemblyCSharp;

[RequireComponent(typeof(AudioSource))]
public class MicroPhoneInput : MonoBehaviour
{
    //==============单例==============// 
    private HomePanelScript homePanelScript;
    private static MicroPhoneInput m_instance;
    public float sensitivity = 100;
    public float loudness;
    private AudioSource playAudio;
    private static string[] micArray;
    const int HEADER_SIZE = 44;
    const int RECORD_TIME = 10;
    List<int> userList;
    private AudioClip redioclip;

    // Use this for initialization
    void Start()
    {
        SocketEventHandle.Instance.micInputReply += MicInputNotice;
        playAudio = GameObject.Find("GamePlayAudio").GetComponent<AudioSource>();
        if (playAudio.clip == null)
        {
            playAudio.clip = AudioClip.Create("playRecordClip", 160000, 1, 8000, false, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //loudness = GetAveragedVolume () * sensitivity;
    }

    public static MicroPhoneInput GetInstance()
    {
        if (m_instance == null)
        {
            micArray = Microphone.devices;
            if (micArray.Length == 0)
            {
                MyDebug.LogError("Microphone.devices is null");
            }

            for (int i = 0; i < Microphone.devices.Length; ++i)
            {
                MyDebug.Log("device name = " + Microphone.devices[i]);
            }

            if (micArray.Length == 0)
            {
                MyDebug.LogError("no mic device");
            }

            var MicObj = new GameObject("MicObj");
            m_instance = MicObj.AddComponent<MicroPhoneInput>();
        }

        return m_instance;
    }

    public void StartRecord(List<int> _userList)
    {
        userList = _userList;
        GetComponent<AudioSource>().Stop();
        if (micArray.Length == 0)
        {
            MyDebug.Log("No Record Device!");
            return;
        }

        //GetComponent<AudioSource>().loop = false;
        //GetComponent<AudioSource>().mute = true;
        redioclip = Microphone.Start("inputMicro", false, RECORD_TIME, 8000); //22050
        while (!(Microphone.GetPosition(null) > 0))
        {
        }

        //	GetComponent<AudioSource>().Play ();
        MyDebug.Log("StartRecord");
        //倒计时
        //StartCoroutine(TimeDown());
    }

    public void StopRecord()
    {
        MyDebug.Log("StopRecord");
        if (micArray.Length == 0)
        {
            MyDebug.Log("No Record Device!");
            return;
        }

        if (!Microphone.IsRecording(null))
        {
            return;
        }

        Microphone.End(null);
        SoundManager.Instance.GamePlayAudio.clip = redioclip;
        ChatSocket.GetInstance.SendMsg(new MicInputRequest(userList, GetClipData()));
        var itemData = new TalkItemData
        {

            name = UIMaJiangPanel.instance.playerItems[0].avatarvo.account.nickname,
            clip = redioclip,
            userId = UIMaJiangPanel.instance.playerItems[0].avatarvo.account.uuid,
            icon = UIMaJiangPanel.instance.playerItems[0].homepanelScript.imgLoad
            
        };
        TalkDataManager.Instance.AddTalkItem(itemData);
        PlayRecord();
    }

    public Byte[] GetClipData()
    {
        if (GetComponent<AudioSource>().clip == null)
        {
            MyDebug.Log("GetClipData audio.clip is null");
            return null;
        }

        var samples = new float[GetComponent<AudioSource>().clip.samples];
        MyDebug.Log("samples.Length = " + samples.Length);
        GetComponent<AudioSource>().clip.GetData(samples, 0);
        var outData = new byte[samples.Length * 2];
        //Int16[] intData = new Int16[samples.Length];
        //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]
        const int rescaleFactor = 32767; //to convert float to Int16
        for (int i = 0; i < samples.Length; i++)
        {
            var temshort = (short) (samples[i] * rescaleFactor);
            var temdata = BitConverter.GetBytes(temshort);
            outData[i * 2] = temdata[0];
            outData[i * 2 + 1] = temdata[1];
        }

        if (outData == null || outData.Length <= 0)
        {
            MyDebug.Log("GetClipData intData is null");
            return null;
        }

        //return intData;
        return outData;
    }


    public void PlayClipData(Int16[] intArr)
    {
        if (intArr.Length == 0)
        {
            MyDebug.Log("get intarr clipdata is null");
            return;
        }

        MyDebug.Log("PlayClipData");
        //从Int16[]到float[]
        var samples = new float[intArr.Length];
        const int rescaleFactor = 32767;
        for (int i = 0; i < intArr.Length; i++)
        {
            samples[i] = (float) intArr[i] / rescaleFactor;
        }

        SoundManager.Instance.GamePlayAudio.clip.SetData(samples, 0);
        SoundManager.Instance.GamePlayAudio.mute = false;
        SoundManager.Instance.GamePlayAudio.Play();
    }

    private void PlayRecord()
    {
        if (SoundManager.Instance.GamePlayAudio.clip == null)
        {
            MyDebug.Log("audio.clip=null");
            return;
        }

        SoundManager.Instance.GamePlayAudio.mute = false;
        SoundManager.Instance.GamePlayAudio.loop = false;
        SoundManager.Instance.GamePlayAudio.Play();
    }

    public float GetAveragedVolume()
    {
        var data = new float[256];
        float a = 0;
        GetComponent<AudioSource>().GetOutputData(data, 0);
        for (int i = 0; i < data.Length; ++i)
        {
            a += Mathf.Abs(data[i]);
        }

        return a / 256;
    }

    private IEnumerator TimeDown()
    {
        var time = 0;
        while (time < RECORD_TIME)
        {
            if (!Microphone.IsRecording(null))
            {
                //如果没有录制
                MyDebug.Log("IsRecording false");
                yield break;
            }

            MyDebug.Log("yield return new WaitForSeconds " + time);
            yield return new WaitForSeconds(1);
            time++;
        }

        if (time >= RECORD_TIME)
        {
            MyDebug.Log("RECORD_TIME is out! stop record!");
            StopRecord();
        }

        yield return 0;
    }

    public void MicInputNotice(ClientResponse response)
    {
        MyDebug.Log("micInputNotice");
        if (GlobalDataScript.soundToggle)
        {
            var data = response.bytes;
            var i = 0;
            var result = new List<short>();
            while (data.Length - i >= 2)
            {
                result.Add(BitConverter.ToInt16(data, i));
                i += 2;
            }

            var arr = result.ToArray(); //这就是你要的
            PlayClipData(arr);
        }
    }

    //save to localhost
    public bool Save(string filename)
    {
        MyDebug.Log("Application.persistentDataPath = " + Application.persistentDataPath);

        var clip = GetComponent<AudioSource>().clip;

        if (!filename.ToLower().EndsWith(".wav"))
        {
            filename += ".wav";
        }

        var filepath = Path.Combine(Application.persistentDataPath, filename);

        MyDebug.Log(filepath);

        // Make sure directory exists if user is saving to sub dir.
        Directory.CreateDirectory(Path.GetDirectoryName(filepath));

        using (FileStream fileStream = CreateEmpty(filepath))
        {

            ConvertAndWrite(fileStream, clip);

            WriteHeader(fileStream, clip);
        }

        return true; // TODO: return false if there's a failure saving the file
    }

    private FileStream CreateEmpty(string filepath)
    {
        var fileStream = new FileStream(filepath, FileMode.Create);
        const byte emptyByte = new byte();

        for (int i = 0; i < HEADER_SIZE; i++) //preparing the header
        {
            fileStream.WriteByte(emptyByte);
        }

        return fileStream;
    }

    private void ConvertAndWrite(FileStream fileStream, AudioClip clip)
    {

        var samples = new float[clip.samples];

        clip.GetData(samples, 0);

        var intData = new Int16[samples.Length];

        var bytesData = new Byte[samples.Length * 2];

        const int rescaleFactor = 32767; //to convert float to Int16

        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short) (samples[i] * rescaleFactor);
            var byteArr = new Byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }

        fileStream.Write(bytesData, 0, bytesData.Length);
    }

    private void WriteHeader(FileStream fileStream, AudioClip clip)
    {

        var hz = clip.frequency;
        var channels = clip.channels;
        var samples = clip.samples;

        fileStream.Seek(0, SeekOrigin.Begin);

        var riff = Encoding.UTF8.GetBytes("RIFF");
        fileStream.Write(riff, 0, 4);

        var chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
        fileStream.Write(chunkSize, 0, 4);

        var wave = Encoding.UTF8.GetBytes("WAVE");
        fileStream.Write(wave, 0, 4);

        var fmt = Encoding.UTF8.GetBytes("fmt ");
        fileStream.Write(fmt, 0, 4);

        var subChunk1 = BitConverter.GetBytes(16);
        fileStream.Write(subChunk1, 0, 4);

        const UInt16 two = 2;
        const UInt16 one = 1;

        var audioFormat = BitConverter.GetBytes(one);
        fileStream.Write(audioFormat, 0, 2);

        var numChannels = BitConverter.GetBytes(channels);
        fileStream.Write(numChannels, 0, 2);

        var sampleRate = BitConverter.GetBytes(hz);
        fileStream.Write(sampleRate, 0, 4);

        var byteRate =
            BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
        fileStream.Write(byteRate, 0, 4);

        var blockAlign = (ushort) (channels * 2);
        fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        const UInt16 bps = 16;
        var bitsPerSample = BitConverter.GetBytes(bps);
        fileStream.Write(bitsPerSample, 0, 2);

        var datastring = Encoding.UTF8.GetBytes("data");
        fileStream.Write(datastring, 0, 4);

        var subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        fileStream.Write(subChunk2, 0, 4);

        //      fileStream.Close();
    }
}