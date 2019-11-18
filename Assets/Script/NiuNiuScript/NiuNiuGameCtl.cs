using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NiuNiuGameCtl : MonoBehaviour
{
    public List<GameObject> shouPai;
    public GameObject faPai;
    public GameObject pai;
    public GameObject uiShouPai;
    private Color32 _color32 = new Color32(255, 255, 255, 255);
    private Color32 color = new Color32(255, 255, 255, 0);

    void Start()
    {
        StartCoroutine(Begin());
    }

    IEnumerator Begin()
    {
        pai.gameObject.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < shouPai.Count; j++)
            {
                iTween.MoveTo(faPai,
                    shouPai[j].GetComponentsInChildren<SpriteRenderer>()[i].gameObject.transform.position, 1f);
                iTween.RotateTo(faPai,
                    shouPai[j].GetComponentsInChildren<SpriteRenderer>()[i].gameObject.transform.eulerAngles, 1f);
                iTween.ScaleTo(faPai,
                    shouPai[j].GetComponentsInChildren<SpriteRenderer>()[i].gameObject.transform.localScale, 1f);
                yield return new WaitForSeconds(1);
                faPai.transform.localPosition = new Vector3(0, 2.32f, -1.35f);
                faPai.transform.localScale = Vector3.one;
                shouPai[j].GetComponentsInChildren<SpriteRenderer>()[i].color = _color32;
            }
        }

        faPai.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            uiShouPai.GetComponentsInChildren<Image>()[i].transform.localEulerAngles = Vector3.zero;
            int cardNum = Random.Range(0, 53);
            
            uiShouPai.GetComponentsInChildren<Image>()[i].sprite =
                Resources.Load("Image/Puke/card_" + cardNum, typeof(Sprite)) as Sprite;
            shouPai[0].GetComponentsInChildren<SpriteRenderer>()[i].color = color;
            uiShouPai.GetComponentsInChildren<Image>()[i].color = _color32;
        }
    }

    IEnumerator DiPai()
    {
        faPai.SetActive(true);
        for (int i = 0; i < shouPai.Count; i++)
        {
            iTween.MoveTo(faPai,
                shouPai[i].GetComponentsInChildren<SpriteRenderer>()[4].gameObject.transform.position, 1f);
            iTween.RotateTo(faPai,
                shouPai[i].GetComponentsInChildren<SpriteRenderer>()[4].gameObject.transform.eulerAngles, 1f);
            iTween.ScaleTo(faPai,
                shouPai[i].GetComponentsInChildren<SpriteRenderer>()[4].gameObject.transform.localScale, 1f);
            yield return new WaitForSeconds(1);
            faPai.transform.localPosition = new Vector3(0, 2.32f, -1.35f);
            faPai.transform.localScale = Vector3.one;
            if (i != 0)
            {
                shouPai[i].GetComponentsInChildren<SpriteRenderer>()[4].color = _color32;
            }
            else
            {
                //生成翻牌UI
            }
        }

        pai.gameObject.SetActive(false);
    }
}