using System;
using UnityEngine;
using UnityEngine.UI;

public class Expression : MonoBehaviour
{
    public GameObject game;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SendExpression);
    }

    public void SendExpression()
    {
        CMD_C_Chat chat = new CMD_C_Chat();
        string mes = "0|" + gameObject.tag;
        Debug.Log(mes);
        chat.szTitle = new byte[100];
        byte[] bt = NetUtil.StringToBytes(mes);
        Array.Copy(bt, chat.szTitle, bt.Length);
        //chat.szTitle = NetUtil.StringToBytes(mes);
        SocketSendManager.Instance.ChewTheRag(chat);
        game.SetActive(false);
    }
}