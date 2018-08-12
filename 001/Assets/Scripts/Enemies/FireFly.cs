using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFly : MonoBehaviour {

    //Used to make different AIStates
    enum Aistates { Idle, Alert, Attack};

    #region Variables

    //Used to find the player 
    private GameObject player;

    //Used to find its own stats
    private EnemyStats stats;

    [SerializeField] private bool debugMode;


    [Header("Enemy State")]
    [SerializeField] private Aistates currentAIState;
    [Space(10)]

    [Header("ParticleSystem")]

    [SerializeField] private ParticleSystem ps;
    [SerializeField] private float hSliderValueR = 0.0f;
    [SerializeField] private float hSliderValueG = 1.0f;
    [SerializeField] private float hSliderValueB = 0.0f;
    [SerializeField] private float hSliderValueA = 1.0f;
    [Space(10)]
   
    [Header("Idle state / movement")]
 
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float Radius;

    private float scale;
    private Vector3 offset;
    private Vector3 centre;
    private float angle;
    [Space(10)]

    [Header("Alert state")]

    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject target;
    [SerializeField] private float orbitSpeed;
    [SerializeField] private float changeStateMinTime, changeStateMaxTime, changeToAttackTime;

    //Finds its own orbit script and fills in the blanks
    private OrbitAround orbitAround;
    [Space(10)]

    [Header("Attack state")]

    //Gameobject works but variables is made in projectile
    [SerializeField] private Projectile projectileAttack;
    [SerializeField] private float attackDelay;

    private float attackDelayUse;
    [Space(10)]

    [Header("Projectile stats")]

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float destroyProjectile;
    [SerializeField] private GameObject targetToShot;

    #endregion

    // Use this for initialization
    void Start ()
    {
        //Used to form the idle movement
        centre = transform.position;
        scale = 2 / (3 - Mathf.Cos(angle * 2));

        //Used to get and use orbit Script
        orbitAround = GetComponent<OrbitAround>();

        //Used to get and use stats
        stats = GetComponent<EnemyStats>();

        //Used to find the player gameobject
        player = GameObject.FindGameObjectWithTag("Player");

        //AttackDelay in attack state
        attackDelayUse = attackDelay;
    }
	
	// Update is called once per frame
	void Update ()
    {
        #region Finding target null
        if (targetToShot == null)
        {
            targetToShot = player;
        }

        if (target == null)
        {
            target = player;
        }
        #endregion

        #region ParticleSystem color change
        //Changes the color of the particlesystem
        var main = ps.main;
        main.startColor = new Color(hSliderValueR, hSliderValueG, hSliderValueB, hSliderValueA);
        #endregion

        #region AIstate
        //Checks what state the firefly is in and what to do then
        if (currentAIState == Aistates.Idle)
        {

            angle += RotateSpeed * Time.deltaTime;

            offset = new Vector3(scale * Mathf.Cos(angle), scale * Mathf.Sin(angle * 2) / 2) * Radius;
            transform.position = centre + offset;
           
            if (Vector3.Distance(target.transform.position, transform.position) <= detectionRange)
            {
                currentAIState = Aistates.Alert;

                changeToAttackTime = Random.Range(changeStateMinTime, changeStateMaxTime);

                hSliderValueG = 0.0f;
                hSliderValueB = 1.0f;
            }

            if (debugMode)
            {
                Debug.Log("FireFly Script / AIstate.Idle");
            }
        }
        else if (currentAIState == Aistates.Alert)
        {
            changeToAttackTime -= Time.deltaTime;

            orbitAround.orbitAroundObject = target;
            orbitAround.speedOrbitOwn = orbitSpeed;
            orbitAround.orbitAroundOwn = true;

            if (changeToAttackTime <= 0)
            {
                currentAIState = Aistates.Attack;

                hSliderValueB = 0.0f;
                hSliderValueR = 1.0f;
            }

            if (Vector3.Distance(target.transform.position, transform.position) > attackRange)
            {
                currentAIState = Aistates.Idle;

                hSliderValueG = 1.0f;
                hSliderValueB = 0.0f;
                hSliderValueR = 0.0f;

                //Sets the idle movement to the new position
                centre = transform.position;
            }
            if (debugMode)
            {
                Debug.Log("FireFly Script / AIstate.Alert");
            }
        }
        else if (currentAIState == Aistates.Attack)
        {
            attackDelayUse -= Time.deltaTime;

            if (attackDelayUse <= 0)
            {                
                var clone = Instantiate(projectileAttack, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                clone.projectileSpeed = projectileSpeed;
                clone.destroyTime = destroyProjectile;
                clone.damage = stats.damage;
                clone.target = targetToShot;
                clone.enemyProjectile = true;

                attackDelayUse = attackDelay;
            }

            if (Vector3.Distance(target.transform.position, transform.position) > attackRange)
            {
                currentAIState = Aistates.Idle;

                hSliderValueG = 1.0f;
                hSliderValueB = 0.0f;
                hSliderValueR = 0.0f;

                //Sets the idle movement to the new position
                centre = transform.position;
            }
            if (debugMode)
            {
                Debug.Log("FireFlyScripts / AIstate.attack");
            }
        }
        #endregion

    }
}
