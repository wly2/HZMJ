using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_MicPhoneScript : MonoBehaviour
{
    public float WholeTime = 10f;
    public GameObject InputGameObject;
    private Boolean btnDown;
    public GameObject circle;
    public UIMaJiangPanel myScript;

    private void Awake()
    {
        myScript = GameObject.Find("Panel_GamePlay").GetComponent<UIMaJiangPanel>();
    }

    void FixedUpdate()
    {
        if (btnDown)
        {
            WholeTime -= Time.deltaTime;
            circle.GetComponent<Slider>().value = WholeTime;
            if (WholeTime <= 0)
            {
                OnPointerUp();
            }
        }
    }

    public void OnPointerDown()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SoundManager.Instance.SetSoundV(PlayerPrefs.GetFloat("SoundVolume", 1));
        if (myScript.avatarList != null && myScript.avatarList.Count > 1)
        {
            btnDown = true;
            InputGameObject.SetActive(true);
            MicroPhoneInput.GetInstance().StartRecord(GetUserList());
        }
        else
        {
            TipsManagerScript.getInstance.setTips("房间里只有你一个人，不能发送语音");
        }
    }

    public void OnPointerUp()
    {
        if (btnDown)
        {
            btnDown = false;
            InputGameObject.SetActive(false);
            WholeTime = 10;
            if (myScript.avatarList != null && myScript.avatarList.Count > 1)
            {
                MicroPhoneInput.GetInstance().StopRecord();
                myScript.MyselfSoundActionPlay();
            }
        }
    }

    private List<int> GetUserList()
    {
        var userList = new List<int>();
        for (int i = 0; i < myScript.avatarList.Count; i++)
        {
            if (myScript.avatarList[i].account.uuid != GlobalDataScript.loginResponseData.account.uuid)
            {
                userList.Add(myScript.avatarList[i].account.uuid);
            }
        }

        return userList;
    }
}