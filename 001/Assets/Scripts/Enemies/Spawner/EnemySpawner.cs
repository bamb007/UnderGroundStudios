using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;

    [Header("Spawner")]
    public int amountToSpawn;
    public GameObject[] whatToSpawn;

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
		if (amountToSpawn > 0)
        {
            SpawnEnemy();
        }
	}
	#endregion

	#region Functions

    public void SpawnEnemy()
    {
        int i = 0;
        i = Random.Range(0, whatToSpawn.Length);
        Instantiate(whatToSpawn[i], this.transform.position, Quaternion.identity);
        amountToSpawn -= 1;
    }

	#endregion
}
