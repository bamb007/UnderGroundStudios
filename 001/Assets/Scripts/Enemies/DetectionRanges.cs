using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRanges : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;

    [SerializeField]
    private float detectionRange, attackRange, saveRange;

    [SerializeField]
    private bool detectionRangeActive, attackRangeActive, saveRangeActive;

    //Used to find player and get range distance
    private GameObject player;

    #endregion

    #region Awake / Start / Update / FixedUpdate / LateUpdate
    // Use this for initialization before game start
    void Awake()
	{
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
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); ;
        }

        if (Vector2.Distance(player.transform.position, transform.position) <= saveRange && saveRangeActive == false)
        {
            saveRangeActive = true;
        }
        else if (Vector2.Distance(player.transform.position, transform.position) > saveRange && saveRangeActive == true)
        {
            saveRangeActive = false;
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= detectionRange && detectionRangeActive == false)
        {
            detectionRangeActive = true;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) > detectionRange && detectionRangeActive == true)
        {
            detectionRangeActive = false;
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange && attackRangeActive == false)
        {
            attackRangeActive = true;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) > attackRange && attackRangeActive == true)
        {
            attackRangeActive = false;
        }
    }
	#endregion

	#region Functions

	#endregion
}
