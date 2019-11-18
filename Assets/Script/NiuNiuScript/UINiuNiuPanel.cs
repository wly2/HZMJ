using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UINiuNiuPanel : MonoBehaviour
{
    public List<GameObject> shouPai;
    public GameObject faPai;
    public GameObject pai;
    public GameObject button;
    private Color32 _color32 = new Color32(255, 255, 255, 255);

    void Start()
    {
        //StartCoroutine(Begin());
    }

    IEnumerator Begin()
    {
        pai.SetActive(true);
        Image image = faPai.GetComponent<Image>();
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < shouPai.Count; i++)
            {
                image.rectTransform.DOMove(shouPai[i].GetComponentsInChildren<Image>()[j].rectTransform.position, 1)
                    .SetEase(Ease.Linear);
                image.rectTransform.DOSizeDelta(shouPai[1].GetComponentsInChildren<Image>()[j].rectTransform.sizeDelta,
                    1);
                yield return new WaitForSeconds(1);
                shouPai[i].GetComponentsInChildren<Image>()[j].color = _color32;
                image.rectTransform.localPosition = Vector3.zero;
                image.rectTransform.sizeDelta = new Vector2(117, 160);
                if (i == 0 && j < 4)
                {
                    shouPai[i].GetComponentsInChildren<Image>()[j].rectTransform.DORotate(new Vector3(0, 0, 0), 1)
                        .SetEase(Ease.Linear);
                    yield return new WaitForSeconds(0.4f);
                    int cardNum = Random.Range(0, 53);
                    shouPai[i].GetComponentsInChildren<Image>()[j].sprite =
                        Resources.Load("Image/Puke/card_" + cardNum, typeof(Sprite)) as Sprite;
                }
            }
        }

        yield return new WaitForSeconds(0.5f);
        pai.SetActive(false);
    }

    public void FanPai()
    {
        StartCoroutine(FanPaiAniamtor());
    }

    IEnumerator FanPaiAniamtor()
    {
        shouPai[0].GetComponentsInChildren<Image>()[4].rectTransform.eulerAngles = new Vector3(0, 180, 0);
        shouPai[0].GetComponentsInChildren<Image>()[4].rectTransform.DORotate(new Vector3(0, 0, 0), 1)
            .SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.4f);
        int cardNum = Random.Range(0, 53);
        shouPai[0].GetComponentsInChildren<Image>()[4].sprite =
            Resources.Load("Image/Puke/card_" + cardNum, typeof(Sprite)) as Sprite;
        button.SetActive(false);
    }
}