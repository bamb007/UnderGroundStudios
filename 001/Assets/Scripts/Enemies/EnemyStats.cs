using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    [SerializeField]
    private float health;

    [SerializeField]
    private float damage;

    [SerializeField]
    private int resistence;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Test death
        health -= Time.deltaTime;

		if (health <= 0)
        {
            Death();
        }
	}

    private void Death()
    {
        Destroy(gameObject);
    }
}
