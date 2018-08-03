using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float health;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
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
    }
}

