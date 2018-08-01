using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    [Header("Object to orbit around")]

    public GameObject orbitAroundObject; //The object we will orbit around

    [Header("Status own")]

    public bool orbitAroundOwn;

    [SerializeField]
    private bool orbitOverOwn;

    [SerializeField]
    private bool stopOrbitOwn;

    private bool endOrbitOwn;

    [Header("Status other")]

    [SerializeField]
    private bool orbitAroundOther;

    [SerializeField]
    private bool orbitOverOther;

    [SerializeField]
    private bool stopOrbitOther;

    private bool endOrbitOther;

    [Header("OtherObjectOrbit")]

    public GameObject[] targetsToOrbit = { };

    [Header("Variables to change orbit")]

    public float speedOrbitOwn;

    [SerializeField]
    private float speedOrbitOther;

    [Header("Delay")]

    [SerializeField]
    private float startDelayOwn;
    [SerializeField]
    private float startDelayOther;
    [SerializeField]
    private float endDelayOwn;
    [SerializeField]
    private float endDelayOther;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        #region OrbitControl

        #region OwnOrbit
        if (startDelayOwn > 0)
        {
            startDelayOwn -= Time.deltaTime;
        }
        else if (startDelayOwn <= 0 && stopOrbitOwn == false)
        {
            Orbit();
            if (stopOrbitOwn == true)
            {
                endDelayOwn -= Time.deltaTime;
                if (endDelayOwn <= 0)
                {
                    endOrbitOwn = true;
                }
            }
        }
        #endregion

        #region OtherOrbit
        if (startDelayOther > 0)
        {
            startDelayOther -= Time.deltaTime;
        }
        else if (startDelayOther <= 0 && stopOrbitOther == false)
        {
            OrbitOther();
            if (stopOrbitOther == true)
            {
                endDelayOther -= Time.deltaTime;
                if (endDelayOther <= 0)
                {
                    endOrbitOther = true;
                }
            }
        }
        #endregion

        #endregion
    }

    private void Orbit()
    {
        if (orbitAroundOwn == true)
        {
            transform.RotateAround(orbitAroundObject.transform.position, Vector3.forward, speedOrbitOwn * Time.deltaTime);
        }

        if (orbitOverOwn == true)
        {
            transform.RotateAround(orbitAroundObject.transform.position, Vector3.right, speedOrbitOwn * Time.deltaTime);
        }

    }

    private void OrbitOther()
    {
        foreach(GameObject go in targetsToOrbit)
        {
            if (orbitAroundOther == true)
            {
                go.transform.RotateAround(this.transform.position, Vector3.forward, speedOrbitOther * Time.deltaTime);
            }

            if (orbitOverOther == true)
            {
                go.transform.RotateAround(this.transform.position, Vector3.right, speedOrbitOther * Time.deltaTime);
            }
        }
    }
}
