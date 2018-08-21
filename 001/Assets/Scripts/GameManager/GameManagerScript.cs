using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class enemies
{
    public string name;
    public int kills;
}
public class GameManagerScript : MonoBehaviour {

    [HideInInspector]
    public int totalKills, currentKills;

    [HideInInspector]
    public List<enemies> allEnemies;

    [HideInInspector]
    public int totalPickup, currentPickup;

    [HideInInspector]
    public float totalPlaytime, currentPlaytime;

    [HideInInspector]
    public int totalScrapmetal;

    [HideInInspector]
    public int weaponsUnlocked, perksUnlocked, itemsUnlocked;

    private static GameManagerScript instance;

    public static GameManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManagerScript>();
            }
            return GameManagerScript.instance;
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void Death(string name)
    {
        totalKills += 1;
        currentKills += 1;

        foreach (enemies enemy in allEnemies)
        {
            if(enemy.name == name)
            {
                enemy.kills += 1;
            }
        }
    }
}
