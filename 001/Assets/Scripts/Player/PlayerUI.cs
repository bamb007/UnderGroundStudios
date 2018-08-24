using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;
	[Space(10)]

    [Header("HealthBar")]

    [SerializeField]
    public Progress_Bar healthBar;

    #region static PlayerUI
    private static PlayerUI instance;

    public static PlayerUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerUI>();
            }
            return PlayerUI.instance;
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
        //Sets the players healthbar
        healthBar.Max = PlayerStats.Instance.maxHealth;
        healthBar.Value = PlayerStats.Instance.currentHealth;
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
