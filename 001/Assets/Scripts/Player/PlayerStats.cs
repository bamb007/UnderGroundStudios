using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;
    [Space(10)]

    [Header("PlayerStats")]
    public int currentHealth;
    public int maxHealth;

    public int baseDamage;
    public int damage;

    public float movementSpeed;
    public float walkSpeed;
    public float runSpeed;

    [Header("Jump")]

    public float jumpHeight;
    public int currentJump;
    public int maxJump;

    [Header("DamageImmunity")]

    public float damageImmunityTime;


    #region Static PlayerStats
    private static PlayerStats instance;

    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerStats>();
            }
            return PlayerStats.instance;
        }

    }
    #endregion

    #endregion

    #region Awake / Start / Update / FixedUpdate / LateUpdate
    // Use this for initialization before game start
    void Awake()
	{
		
	}

	// Use this for initialization
	void Start() 
	{
        currentHealth = maxHealth;
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
