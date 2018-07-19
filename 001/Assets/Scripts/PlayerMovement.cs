using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
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
}

