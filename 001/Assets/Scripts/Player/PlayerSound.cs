using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSound : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;
	[Space(10)]

    [Header("SoundEffects")]

    public AudioSource audioS;
    public AudioSource jumpSound;

    private static PlayerSound instance;

    public static PlayerSound Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerSound>();
            }
            return PlayerSound.instance;
        }
    }

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

	#endregion
}
