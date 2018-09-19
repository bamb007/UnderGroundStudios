using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterTime : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;

    public float time;

	#endregion

	#region Awake / Start / Update / FixedUpdate / LateUpdate
	// Use this for initialization before game start
	void Awake()
	{
		
	}

	// Use this for initialization
	void Start() 
	{
		
	}
	
	// FixedUpdate is called just before physic update
	void FixedUpdate()
	{
		
	}

	// LateUpdate is called after FixedUpdate and Update
	void LateUpdate()
	{
		
	}

	// Update is called once per frame
	void Update() 
	{
		if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
	}
	#endregion

	#region Functions

	#endregion
}
