using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private int projectileSpeed;

    [SerializeField]
    private float destroyTime;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);

        Destroy(gameObject, destroyTime);
	}
}
