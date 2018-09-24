using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(ProjectileStats))]
public class AIAttack : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;

    private ProjectileStats proStats;
    private EnemyStats stats;

    private GameObject targetToShot;

    #endregion

    #region Awake / Start / Update / FixedUpdate / LateUpdate
    // Use this for initialization before game start
    void Awake()
	{
        proStats = GetComponent<ProjectileStats>();
        stats = GetComponent<EnemyStats>();
        targetToShot = GameObject.FindGameObjectWithTag("Player");
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

    public void Attack()
    {
        var clone = Instantiate(proStats.projectileAttack, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        clone.projectileSpeed = proStats.Speed;
        clone.destroyTime = proStats.destroyTime;
        clone.damage = stats.damage;
        clone.target = targetToShot;
        clone.enemyProjectile = proStats.enemyProjectile;
        clone.playerProjectile = proStats.playerProjectile;
        clone.canCollideWithWall = proStats.canCollideWithWall;
        clone.ground = proStats.ground;
    }

    #endregion
}
