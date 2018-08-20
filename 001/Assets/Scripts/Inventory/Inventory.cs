using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItems
{
    public int itemID;
    public string itemName;
    public string itemInfo;
    public int itemAmount;
    //public Sprite itemImage;
}

public class Inventory : MonoBehaviour
{
    public List<InventoryItems> inventory;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.I))
        {
            DisplayInventory();
        }
	}

    void DisplayInventory()
    {
        foreach(InventoryItems II in inventory)
        {
            Debug.Log(II);
        }
    }
}
