using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    public float projectileSpeed;

    public float damage;

    public float destroyTime;

    public GameObject target;

    // Use this for initialization
    void Start()
    {
        // Aim bullet in player's direction.
        transform.LookAt(target.transform.position);
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("PlayerProjectile / Projectile hit enemy");
            Destroy(gameObject);
        }
    }
}
