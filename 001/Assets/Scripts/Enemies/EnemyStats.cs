using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public float health;

    public int damage;

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
        gameObject.GetComponent<MyLootTable>().SpawnItem(1);
        Destroy(gameObject);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }
}
