using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour {

    private SpriteRenderer hiddenObj;
    public GameObject hidden;
    
	// Use this for initialization
	void Start () {

        hiddenObj = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            hiddenObj.enabled = true;
        }

        /*
        if (collision.gameObject.tag == "Player")
        {
            hiddenObj.enabled = true;
        }
        */
    }
}
