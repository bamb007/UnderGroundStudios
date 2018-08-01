using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIStatesClass
{
    public string stateName;
    [SerializeField]
    private MonoBehaviour Script;
}

public class AIState : MonoBehaviour {

    [SerializeField]
    private AIStatesClass[] aiState;

    [SerializeField]
    private string currentAIState;

    private int currentArrayID;

    private AIMoveTo test;

	// Use this for initialization
	void Start ()
    {
        test = GetComponent<AIMoveTo>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		foreach (AIStatesClass state in aiState)
        {
            if (currentAIState == aiState[currentArrayID].stateName)
            {

            }

            currentArrayID += 1;
        }
	}
}
