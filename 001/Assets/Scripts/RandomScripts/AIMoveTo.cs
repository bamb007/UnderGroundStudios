using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveTo : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private int xOffset;
    [SerializeField]
    private int yOffset;
    [SerializeField]
    private int zOffset;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckDistanceToTarget();
	}

    private void CheckDistanceToTarget()
    {
        if (target.transform.position.x - transform.position.x + xOffset <= 0.05 && target.transform.position.x - transform.position.x + xOffset >= -0.05)
        {
            Debug.Log("X do not move");
        }
        else if (target.transform.position.x - transform.position.x + xOffset < 1)
        {
            transform.position += new Vector3(-1 * movementSpeed, 0, 0);
            Debug.Log("+X is True");
        }
        else if (target.transform.position.x - transform.position.x + xOffset> -1 )
        {
            transform.position += new Vector3(1 * movementSpeed, 0, 0);
            Debug.Log("-X is true");
        }

        if (target.transform.position.y - transform.position.y + yOffset <= 0.05 && target.transform.position.y - transform.position.y + yOffset >= -0.05)
        {
            Debug.Log("Y  do not move");
        }
        else if (target.transform.position.y - transform.position.y + yOffset < 1)
        {
            transform.position += new Vector3(0, -1 * movementSpeed, 0);
            Debug.Log("+Y is true");
        }
        else if(target.transform.position.y - transform.position.y + yOffset > -1)
        {
            transform.position += new Vector3(0, 1 * movementSpeed, 0);
            Debug.Log("-Y is true");
        }
        
    }
}
