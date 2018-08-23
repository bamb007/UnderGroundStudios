using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour {

    public float maxHealth = 100;
    public float currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            maxHealth -= 10;

            currentHealth = maxHealth;

            if (currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
