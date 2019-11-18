using UnityEngine;

public class RecordBtnClick : MonoBehaviour
{
    public GameObject game1;
    public GameObject game2;
    public GameObject game3;

    public void BtnClickOne()
    {
        game1.SetActive(true);
        game2.SetActive(false);
        game3.SetActive(false);
    }

    public void BtnClickTwo()
    {
        game1.SetActive(false);
        game2.SetActive(true);
        game3.SetActive(false);
    }

    public void BtnClickThree()
    {
        game1.SetActive(false);
        game2.SetActive(false);
        game3.SetActive(true);
    }

    public void Close()
    {
        UIPanel_Report.instance.game.SetActive(true);
        UIPanel_Report.instance.recordPanel.SetActive(false);
    }
}