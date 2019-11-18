using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIPanel_UserInfo : UIWindow
{
    public Image headIcon;
    public Text ip;
    public Text id;
    public new Text name;
    public Image sex;
    public Text playCount;
    public Text winCount;
    private Texture2D texture2D;
    public Text estimate;
    public Text appraise;
    public List<Sprite> sexImage;

    public void SetUIData()
    {
        headIcon.preserveAspect = false;
        
        headIcon.sprite = GlobalDataScript.weChatInformation.headIcon;
        headIcon.preserveAspect = true;
        id.text = GlobalDataScript.weChatInformation.id.ToString();
        name.text = GlobalDataScript.weChatInformation.weChatName;
        ip.text = NetUtil.BytesToString(GlobalDataScript.weChatInformation.ip);
        sex.sprite = sexImage[GlobalDataScript.weChatInformation.sex];
        sex.SetNativeSize();
        sex.gameObject.GetComponent<RectTransform>().sizeDelta *= 1.5f;
    }

    public void Copy()
    {
        UnityPhoneManager.Instance.SetCopy(id.text);
    }
}