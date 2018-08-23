using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Player or enemy")]

    public bool playerProjectile;

    public bool enemyProjectile;

    [Header("ProjectileStats")]
    //Finds the players stats
    private PlayerController playerStats;

    public float projectileSpeed;

    public int damage;

    public float destroyTime;

    //Used to target a specific gameobject
    public GameObject target;

    //Used to use variables from player
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        //Finds the player
        player = GameObject.FindGameObjectWithTag("Player");

        playerStats = player.GetComponent<PlayerController>();

        //Aim bullet in player's direction.

        if (enemyProjectile)
        {
            transform.LookAt(target.transform.position);
        }

        if (playerProjectile)
        {
            transform.rotation = Quaternion.LookRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;

        Destroy(gameObject, destroyTime);
    }

    //Collision check and action
    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerProjectile)
        {
            if (other.gameObject.CompareTag("Crate"))
            {
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
                Debug.Log("Projectile / enemy hit");
            }
        }

        if (enemyProjectile)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerStats.TakeDamage(damage);

                Debug.Log("Projectile / Projectile hit player");
                Destroy(gameObject);
            }
        }

    }
}
