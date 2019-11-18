using UnityEngine;

public class RecordButton : MonoBehaviour
{
    public void Btn()
    {
        UIPanel_Report.instance.recordPanel.SetActive(true);
        UIPanel_Report.instance.game.SetActive(false);
    }
}