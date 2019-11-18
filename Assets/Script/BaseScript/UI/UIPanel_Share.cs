using UnityEngine;

public class UIPanel_Share : UIWindow
{
    public void WeChatShare()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SoundManager.Instance.SetSoundV(PlayerPrefs.GetFloat("SoundVolume", 1));
        UnityPhoneManager.Instance.ShareSessionText("sss");
    }

    public void PengYouQuanShare()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SoundManager.Instance.SetSoundV(PlayerPrefs.GetFloat("SoundVolume", 1));
        UnityPhoneManager.Instance.ShareTimelineText("ss");
    }
}