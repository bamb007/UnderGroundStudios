using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsToDrop
{
    public GameObject[] items;

    // How many units the item takes - more units, higher chance of being picked
    public float probabilityWeight;

    // Displayed only as an information for the designer/programmer. Should not be set manually via inspector!    
    public float probabilityPercent;

    public bool active;

    [HideInInspector]
    public float probabilityRangeFrom;
    [HideInInspector]
    public float probabilityRangeTo;
}

public class MyLootTable : MonoBehaviour
{

    public ItemsToDrop[] lootTable;

    // Sum of all weights of items.
    float probabilityTotalWeight;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ValidateTable()
    {

            float currentProbabilityWeightMaximum = 0f;
        /*
            // Sets the weight ranges of the selected items.
            foreach (GameObject lootDropItem in lootTable)
            {

                if (lootTable.probabilityWeight < 0f)
                {
                    // Prevent usage of negative weight.
                    Debug.Log("You can't have negative weight on an item. Reseting item's weight to 0.");
                    lootDropItem.probabilityWeight = 0f;
                }
                else
                {
                    lootDropItem.probabilityRangeFrom = currentProbabilityWeightMaximum;
                    currentProbabilityWeightMaximum += lootDropItem.probabilityWeight;
                    lootDropItem.probabilityRangeTo = currentProbabilityWeightMaximum;
                }

            }

            probabilityTotalWeight = currentProbabilityWeightMaximum;

            // Calculate percentage of item drop select rate.
            foreach (T lootDropItem in lootDropItems)
            {
                lootDropItem.probabilityPercent = ((lootDropItem.probabilityWeight) / probabilityTotalWeight) * 100;
            }
            */
    }
}
