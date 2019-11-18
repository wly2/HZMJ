using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testIEnumerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Test());
        TestInvoke();
        testSum();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(3);
        Debug.LogError("3");

        yield return new WaitForSeconds(6);
        Debug.LogError("6");
    }

    private void TestInvoke()
    {
        Invoke("testFour", 4);
    }

    private void testFour()
    {
        Debug.LogError("4");
    }

    private void testSum()
    {
        int i = 1;
        i++;
        Debug.LogError("testSum" + i);
        for (int sum = 0; sum < 10; sum++)
        {
            //sum++;
            Debug.LogError("sum" + sum );
        }
    }
}
