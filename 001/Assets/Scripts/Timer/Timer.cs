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
        int minutes = (int)t / 60;
        int seconds = (int)t % 60;
        string minute = minutes.ToString();
        string second = seconds.ToString();

        if (minutes < 10)
        {
            minute = "0" + minute;
        }

        if (seconds < 10)
        {
            second = "0" + second;
        }

        timerText.text = minute + ":" + second; 
	}
}
