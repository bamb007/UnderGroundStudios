using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private static float timeSinsTheBeginning = 0;
    private float minutes = 0;
    private float hours = 0;

    [SerializeField]
    private Text timerText;
	
	void Start ()
    {
        timeSinsTheBeginning += Time.time;
       // StartCoroutine(IncreasTime());
	}
	
	
	void Update () {

        float t = Time.time - timeSinsTheBeginning;
        string minutes = ((int)t / 60).ToString();
        string seconds = Mathf.Round(t % 60).ToString();

        timerText.text = "Minutes: " + minutes + " : " + "Seconds: " + seconds; 
	}
    /*
    IEnumerator IncreasTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeSinsTheBeginning++;

            timerText.text = "Seconds: " + timeSinsTheBeginning;
        }
    }*/
}
