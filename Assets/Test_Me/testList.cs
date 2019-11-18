using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testList : MonoBehaviour {
    int i = 10;
	// Use this for initialization
	void Start () {
        Test();
        Test2();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Test()
    {
        i -= 1;
        Debug.LogError(i);
    }
    private void Test2()
    {
        i = i - 1;
        Debug.LogError(i);
    }
}
