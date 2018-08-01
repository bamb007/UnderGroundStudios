using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFly : MonoBehaviour {

    enum Aistates { Idle, Alert, Attack};

    #region Variables
    [Header("Enemy State")]
    [SerializeField]
    private Aistates currentAIState;

    [Header("ParticleSystem")]

    [SerializeField]
    private ParticleSystem ps;
    [SerializeField]
    private float hSliderValueR = 0.0f;
    [SerializeField]
    private float hSliderValueG = 1.0f;
    [SerializeField]
    private float hSliderValueB = 0.0f;
    [SerializeField]
    private float hSliderValueA = 1.0f;


    [Header("Idle state / movement")]

    [SerializeField]
    private float RotateSpeed;

    [SerializeField]
    private float Radius;

    private float scale;
    private Vector3 offset;
    private Vector3 centre;
    private float angle;

    [Header("Alert state")]

    [SerializeField]
    private float detectionRange;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float orbitSpeed;

    [SerializeField]
    private float minTime;

    [SerializeField]
    private float maxTime;

    [SerializeField]
    private float attackTime;

    //Finds its own orbit script and fills in the blanks
    private OrbitAround orbitAround;

    [Header("Attack state")]

    [SerializeField]
    private GameObject projectileAttack;
    [SerializeField]
    private float attackDelay;

    private float attackDelayUse;

    #endregion

    // Use this for initialization
    void Start ()
    {
        //Used to form the idle movement
        centre = transform.position;
        scale = 2 / (3 - Mathf.Cos(angle * 2));

        //Used to get and use orbit Script
        orbitAround = GetComponent<OrbitAround>();

        //AttackDelay in attack state
        attackDelayUse = attackDelay;
    }
	
	// Update is called once per frame
	void Update ()
    {
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

                attackTime = Random.Range(minTime, maxTime);

                hSliderValueG = 0.0f;
                hSliderValueB = 1.0f;
            }

            Debug.Log("FireFly Script / AIstate.Idle");
        }
        else if (currentAIState == Aistates.Alert)
        {
            attackTime -= Time.deltaTime;

            orbitAround.orbitAroundObject = target;
            orbitAround.speedOrbitOwn = orbitSpeed;
            orbitAround.orbitAroundOwn = true;

            if (attackTime <= 0)
            {
                currentAIState = Aistates.Attack;

                hSliderValueB = 0.0f;
                hSliderValueR = 1.0f;
            }

            if (Vector3.Distance(target.transform.position, transform.position) > detectionRange)
            {
                currentAIState = Aistates.Idle;

                hSliderValueG = 1.0f;
                hSliderValueB = 0.0f;
                hSliderValueR = 0.0f;
            }

            Debug.Log("FireFly Script / AIstate.Alert");
        }
        else if (currentAIState == Aistates.Attack)
        {
            attackDelayUse -= Time.deltaTime;

            if (attackDelayUse <= 0)
            {
                Instantiate(projectileAttack, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);         

                attackDelayUse = attackDelay;
            }

            if (Vector3.Distance(target.transform.position, transform.position) > detectionRange)
            {
                currentAIState = Aistates.Idle;

                hSliderValueG = 1.0f;
                hSliderValueB = 0.0f;
                hSliderValueR = 0.0f;
            }

            Debug.Log("FireFlyScripts / AIstate.attack");
        }
        #endregion
    }
}
