using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public float health;

    public float damage;

    public float movementSpeed;

    public float resistance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Test death
        //health -= Time.deltaTime;

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
