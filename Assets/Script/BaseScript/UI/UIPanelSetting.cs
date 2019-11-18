using UnityEngine;
using UnityEngine.UI;

public class UIPanelSetting : UIWindow
{
    public Slider soundSlider;
    public Slider musicSlider;

    void Start()
    {
        soundSlider.value = SoundManager.Instance.GetSoundV();
        musicSlider.value = SoundManager.Instance.GetMusicV();
    }

    public void OnMusicChange(float v)
    {
        SoundManager.Instance.SetMusicV(v);
    }

    public void OnSoundChange(float v)
    {
        SoundManager.Instance.SetSoundV(v);
    }

    public void ExitButton()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SoundManager.Instance.SetSoundV(PlayerPrefs.GetFloat("SoundVolume", 1));
        MySceneManager.instance.SceneToLogIn();
    }
    /// <summary>
    /// 重写Close()
    /// </summary>
    public override void CloseUI()
    {
        base.CloseUI();

    }
}