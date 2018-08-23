using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotation : MonoBehaviour
{
    [Header("Status functions")]

    //Used to check if the rotation should stop and if true then activates endDelay
    [SerializeField]
    private bool stopRotationOwn;
    [SerializeField]
    private bool stopRotationOther;

    //Used to end the rotation of the script
    private bool endRotationOwn;
    private bool endRotationOther;

    [Header("Delay")]

    [SerializeField]
    private float startDelayOwn;
    [SerializeField]
    private float startDelayOther;
    [SerializeField]
    private float endDelayOwn;
    [SerializeField]
    private float endDelayOther;


    [Header("Own Rotation")]

    [SerializeField]
    private float xRotation;
    [SerializeField]
    private float yRotation;
    [SerializeField]
    private float zRotation;

    [Header("OtherObjectRotation")]

    public GameObject[] targetsToRotate = {};

    [SerializeField]
    private float xRotationTarget;
    [SerializeField]
    private float yRotationTarget;
    [SerializeField]
    private float zRotationTarget;

    [Header("TestArea")]

    [SerializeField]
    private int testInt;
       
    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        //Controls when to start and end rotation and how much rotation on each axis
        #region RotationControl (Own and other)

        #region OwnRotation
        if (startDelayOwn > 0)
        {
            startDelayOwn -= Time.deltaTime;
        }
        else if (startDelayOwn <= 0 && endRotationOwn == false)
        {
            RotateObject();
            if (stopRotationOwn == true)
            {
                endDelayOwn -= Time.deltaTime;
                if (endDelayOwn <= 0)
                {
                    endRotationOwn = true;
                }
            }
        }
        #endregion

        #region OtherRotation
        if (startDelayOther > 0)
        {
            startDelayOther -= Time.deltaTime;
        }
        else if (startDelayOther <= 0 && endRotationOther == false)
        {
            RotateOtherObject();
            if (stopRotationOther == true)
            {
                endDelayOther -= Time.deltaTime;
                if (endDelayOther <= 0)
                {
                    endRotationOther = true;
                }
            }
        }
        #endregion

        #endregion
    }

    private void RotateObject()
    {
        transform.Rotate(xRotation, yRotation, zRotation);
    }

    private void RotateOtherObject()
    {
        foreach(GameObject go in targetsToRotate)
        {
            go.transform.Rotate(xRotationTarget, yRotationTarget, zRotationTarget);
        }
        
    }

}
