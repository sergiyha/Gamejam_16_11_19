using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatabolaTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	 //   var i = GetComponent<ParabolaController>().GetPositionAtTime(1);
		//Debug.LogError(i);
	    StartCoroutine(AfterStart());

    }

	private IEnumerator AfterStart()
	{
		yield return null;
		var i = GetComponent<ParabolaController>().GetPositionAtTime(1);
		Debug.LogError(i);
	}

	// Update is called once per frame
    void Update()
    {
        
    }
}
