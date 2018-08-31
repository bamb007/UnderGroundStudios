using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActiveOnClick : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;

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
		
	}
	#endregion

	#region Functions

    public void ToggleActive(GameObject target)
    {
        Debug.Log(target.activeSelf);
        target.SetActive(!target.activeSelf);
        Debug.Log(target.activeSelf);
    }

    #endregion
}
