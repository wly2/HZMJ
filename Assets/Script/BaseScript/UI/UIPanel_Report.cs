using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPanel_Report : UIWindow
{
    public static UIPanel_Report instance;
    public List<RecordItem> speakList;
    [SerializeField] ScrollRectList myScrollRect;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject rankItem;
    public GameObject game;
    public GameObject recordPanel;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        game.SetActive(true);
        recordPanel.SetActive(false);
        InitScroll();
    }

    void OnValueChange(Vector2 pos)
    {
        myScrollRect.OnValueChange(pos);
    }
    
    /// <summary>
    /// 生成战绩下拉列表
    /// </summary>
    void InitScroll()
    {
        speakList = RecordManager.Instance.List;
        scrollRect.onValueChanged.AddListener(OnValueChange);
        myScrollRect.createitemobject = delegate(int index, UnityAction<GameObject> action)
        {
            if (rankItem != null)
            {
                //GameObject cellObj = Instantiate(rankItem);
                //action(cellObj);
            }
        };
        myScrollRect.updateItem = delegate(ItemCell o, int index)
        {
            o.item.GetComponentsInChildren<Text>()[0].text = speakList[index].id.ToString();
            o.item.GetComponentsInChildren<Text>()[1].text = speakList[index].roomId.ToString();
            o.item.GetComponentsInChildren<Text>()[2].text = speakList[index].roomOwn;
            o.item.GetComponentsInChildren<Text>()[3].text = speakList[index].gameInnings.ToString();
            o.item.GetComponentsInChildren<Text>()[4].text = speakList[index].gameTime;
        };

        myScrollRect.Init(speakList.Count);
        scrollRect.inertia = true;
    }
}