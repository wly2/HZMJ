using UnityEngine;
using System.Collections;

public class testLocalWorld : MonoBehaviour
{


    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 50), "世界坐标移动"))
        {
            this.gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1
                , gameObject.transform.position.y
                , gameObject.transform.position.z);
        }

        if (GUI.Button(new Rect(0, 50, 200, 50), "本地坐标移动"))
        {
            this.gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + 1
                , gameObject.transform.localPosition.y
                , gameObject.transform.localPosition.z);
        }

        GUI.Label(new Rect(210, 0, 300, 50), "世界坐标:" + string.Format("({0},{1},{2})", gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
        GUI.Label(new Rect(210, 50, 300, 50), "本地坐标:" + string.Format("({0},{1},{2})", gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z));


    }

}