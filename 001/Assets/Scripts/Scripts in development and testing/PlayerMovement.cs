using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float health;

    private Rigidbody2D rb2d;

    [Header("Projectile stats")]

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float destroyProjectile;

    [SerializeField]
    private GameObject targetToShot;

    [SerializeField]
    private Projectile projectileAttack;

    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        float moveHori = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHori, moveVert);
        rb2d.AddForce(movement * movementSpeed);
        
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player attacking");
            var clone = Instantiate(projectileAttack, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            clone.projectileSpeed = projectileSpeed;
            clone.destroyTime = destroyProjectile;
            clone.target = targetToShot;
            clone.playerProjectile = true;
        }

        Death();
	}

    private void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
       if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1 * movementSpeed, 0, 0);
        }
       else if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1 * movementSpeed, 0, 0);
        }
       else if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 1 * movementSpeed, 0);
        }
       else if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -1 * movementSpeed, 0);
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log("Player took " + dmg + " damage and health is now " + health + " HP");     
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {

    }
    
}

