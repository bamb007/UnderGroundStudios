using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour 
{

    enum AIStates { Patrol, Attack };

    #region Fields

    [Header("DEBUG")]
	[SerializeField] private bool debugmode;

    //Used to find the player 
    private GameObject player;

    //Used to find its own stats
    private EnemyStats stats;

    private ProjectileStats proStats;

    private DetectEdge edgeDetection;

    private TeleportBack teleBack;

    public bool bossMode;

    [Header("Movement")]


    [SerializeField]
    private float detectionRange;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float saveRange;
    [SerializeField]
    private GameObject target;
    private GameObject targetToShot;

    private AIStates aiState;

    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private float attackPerSec;

    [SerializeField]
    private float teleportBack;


    #endregion

    #region Awake / Start / Update / FixedUpdate / LateUpdate
    // Use this for initialization before game start
    void Awake()
	{
        //Used to get and use stats
        stats = GetComponent<EnemyStats>();

        proStats = GetComponent<ProjectileStats>();

        edgeDetection = GetComponent<DetectEdge>();

        teleBack = GetComponent<TeleportBack>();
    }

	// Use this for initialization
	void Start() 
	{

        //Used to find the player gameobject
        player = GameObject.FindGameObjectWithTag("Player");
        aiState = AIStates.Patrol;

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
        if (targetToShot == null)
        {
            targetToShot = player;
        }

        if (target == null)
        {
            target = player;
        }

        if (edgeDetection == null)
        {
            edgeDetection = GetComponent<DetectEdge>();         
        }

        if (teleBack == null)
        {
            teleBack = GetComponent<TeleportBack>();
        }

        if (!bossMode)
        {
            if (aiState == AIStates.Attack)
            {
                edgeDetection.active = false;
            }
            else if (aiState == AIStates.Patrol)
            {
                edgeDetection.active = true;
            }
        }


        
        if (Vector2.Distance(target.transform.position, transform.position) <= saveRange)
        {
            if (bossMode)
            {

            }
        }
        else if (Vector3.Distance(target.transform.position, transform.position) <= detectionRange && aiState == AIStates.Patrol)
        {
            aiState = AIStates.Attack;
        }
        else if (Vector3.Distance(target.transform.position, transform.position) <= attackRange && aiState == AIStates.Attack)
        {
            if (attackDelay <= 0)
            {
                Attack();
                attackDelay = attackPerSec;
            }
            else if (attackDelay > 0)
            {
                attackDelay -= Time.deltaTime;
            }

            if (bossMode)
            {
                if (teleportBack <= 0)
                {
                    TeleportBack();
                    teleportBack += Random.Range(2, 7);
                }
                else if (teleportBack > 0)
                {
                    teleportBack -= Time.deltaTime;
                }
            }

            if (bossMode && Vector2.Distance(target.transform.position, transform.position) <= saveRange)
            {
                if (player.transform.position.x > transform.position.x)
                {
                    transform.Translate(new Vector3(-0.1f, 0, 0));
                }
                else if (player.transform.position.x < transform.position.x)
                {
                    transform.Translate(new Vector3(0.1f, 0, 0));
                }
            }
        }
        else
        {
            aiState = AIStates.Patrol;
        }
    }
	#endregion

	#region Functions

    private void Attack()
    {
        var clone = Instantiate(proStats.projectileAttack, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        clone.projectileSpeed = proStats.Speed;
        clone.destroyTime = proStats.destroyTime;
        clone.damage = stats.damage;
        clone.target = targetToShot;
        clone.enemyProjectile = true;
        clone.canCollideWithWall = proStats.canCollideWithWall;
        clone.ground = proStats.ground;
    }

    private void TeleportBack()
    {
        teleBack.active = true;
    }

	#endregion
}
