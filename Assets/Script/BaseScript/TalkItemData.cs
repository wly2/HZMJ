using UnityEngine;

public class TalkItemData
{
    public TalkItemData()
    {

    }

    public TalkItemData(int id, string message)
    {
        this.id = id;
        this.message = message;
    }

    public int id;
    public string message;
    public string name;
    public int userId;
    public Sprite icon;
    public Sprite faceSprite;
    public AudioClip clip;
}