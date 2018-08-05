using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    //Finds the players stats
    private PlayerMovement playerStats;

    public float projectileSpeed;

    public float damage;

    public float destroyTime;

    //Used to use variables from player
    private GameObject player;

    public GameObject target;

	// Use this for initialization
	void Start ()
    {
        //Finds the player
        player = GameObject.FindGameObjectWithTag("Player");

        playerStats = player.GetComponent<PlayerMovement>();

        // Aim bullet in player's direction.
        transform.LookAt(target.transform.position);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;

        Destroy(gameObject, destroyTime);
	}

    //Collision check and action
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerStats.TakeDamage(damage);

            Debug.Log("Projectile / Projectile hit player");
            Destroy(gameObject);
        }
    }
}
