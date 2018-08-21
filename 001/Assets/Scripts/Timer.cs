using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private int timeSinsTheBeginning = 0;

    [SerializeField]
    private Text timerText;
	
	void Start () {
        StartCoroutine(IncreasTime());
	}
	
	
	void Update () {
		
	}

    IEnumerator IncreasTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeSinsTheBeginning++;

            timerText.text = "Time: " + timeSinsTheBeginning;
        }
    }
}
