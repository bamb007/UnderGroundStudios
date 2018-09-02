using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;

    [Space(10)]
    [Header("SpawnFrom")]

    public bool playerProjectile;
        
    public bool enemyProjectile;

    [Header("Collision")]

    public bool canCollideWithWall;

    public LayerMask ground;

    [Space(10)]
    [Header("Projectile stats")]

    public float Speed;

    public float destroyTime;

    public GameObject targetToShot;

    public Projectile projectileAttack;

    //Used to find the player 
    private GameObject player;

    #endregion

    #region Awake / Start / Update / FixedUpdate / LateUpdate
    // Use this for initialization before game start
    void Awake()
	{
        //Used to find the player gameobject
        player = GameObject.FindGameObjectWithTag("Player");
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

	#endregion
}
