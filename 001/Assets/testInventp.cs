using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testInventp : MonoBehaviour {

    public GameObject g;

    public GameObject achievement;

	// Use this for initialization
	void Start () {
        achievement = (GameObject)Instantiate(g);
        achievement.transform.SetParent(GameObject.Find("Test").transform);
        achievement.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        achievement.transform.GetChild(0).GetComponent<Text>().text = "4";
        achievement.transform.GetChild(1).GetComponent<Text>().text = "fre";
        achievement.transform.GetChild(2).GetComponent<Text>().text = "fgr";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
