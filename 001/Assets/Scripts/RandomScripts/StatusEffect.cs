using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    [Header("Status effects")]

    [SerializeField]
    private bool frozen;

    [SerializeField]
    private bool burn;

    [Header("Frozen")]

    [SerializeField]
    private float frozenTime;

    [SerializeField]
    private float slowProcent;

    [Header("Burn")]

    [SerializeField]
    private float burnTime;

    [SerializeField]
    private float burnDamage;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (frozen == true)
        {
            Frozen();
        }
        
        if (burn == true)
        {
            Burn();
        }
	}

    private void Frozen()
    {

    }

    private void Burn()
    {

    }
}
