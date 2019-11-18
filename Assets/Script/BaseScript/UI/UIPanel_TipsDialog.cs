using UnityEngine.UI;

public class UIPanel_TipsDialog : UIWindow
{
	public Text tipsText;

	public void SetMes(string mes)
	{
		tipsText.text = mes;
	}
}