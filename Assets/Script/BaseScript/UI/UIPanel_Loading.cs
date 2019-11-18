using AssemblyCSharp;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_Loading : UIWindow
{
    public static UIPanel_Loading instance;
    public GameObject go;
    public Slider slider;
    public int percentV;
    public Text percentText;
    public bool isLoadResource;
    public Text tipText;
    private bool showpercent;
    private string tipMes;

    public GameObject bg;

    void OnEnable()
    {
        playCloseSound = false;
        tipText.text = "";
        showpercent = false;
        instance = this;
        MyDebug.Log("ShowUILoading");
        GameMessageManager.CloseLoading += CloseUI;
        percentV = 0;
        slider.gameObject.SetActive(false);
        bg.SetActive(false);
    }

    void Update()
    {
        if (showpercent)
        {
            slider.gameObject.SetActive(true);
            bg.SetActive(true);
            tipText.text = tipMes;
            showpercent = false;
        }

        go.transform.Rotate(new Vector3(0, 0, -90 * Time.deltaTime * 5));
        slider.value = percentV * 0.01f;
        percentText.text = percentV + "%";
    }

    private void OnDisable()
    {
        instance = null;
        MyDebug.Log("CloseUILoading");
        GameMessageManager.CloseLoading -= CloseUI;
        slider.gameObject.SetActive(false);
        bg.SetActive(false);
    }

    public void SetLoadPercentShow(string mes)
    {
        tipMes = mes;
        showpercent = true;
    }
}