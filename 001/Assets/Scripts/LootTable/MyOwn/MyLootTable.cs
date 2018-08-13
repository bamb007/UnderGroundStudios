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
    public List<ItemsToDrop> lootTable;

    // Sum of all weights of items.
    float probabilityTotalWeight;

    // Use this for initialization
    void Start()
    {
        PickLootDropItem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnValidate()
    {
        LootTableCheck();
    }

    public void LootTableCheck()
    {
        #region Checking weight on all items in loottable

        if (lootTable != null && lootTable.Count > 0)
        {
            float currentProbabilityWeightMaximum = 0f;

            foreach (ItemsToDrop itd in lootTable)
            {
                if (itd.probabilityWeight < 0f)
                {
                    // Prevent usage of negative weight.
                    Debug.Log("You can't have negative weight on an item. Reseting item's weight to 0.");
                    itd.probabilityWeight = 0f;
                }
                else
                {
                    itd.probabilityRangeFrom = currentProbabilityWeightMaximum;
                    currentProbabilityWeightMaximum += itd.probabilityWeight;
                    itd.probabilityRangeTo = currentProbabilityWeightMaximum;
                }
            }

            probabilityTotalWeight = currentProbabilityWeightMaximum;

            // Calculate percentage of item drop select rate.
            foreach (ItemsToDrop itd in lootTable)
            {
                itd.probabilityPercent = ((itd.probabilityWeight) / probabilityTotalWeight) * 100;
            }
        }
        #endregion

        #region Code to acces all variable in list
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject g1;
            GameObject g2;

            foreach (ItemsToDrop itd in lootTable)
            {
                if (itd.active == true)
                {
                    g1 = null;
                    g2 = null;

                    foreach (GameObject go in itd.items)
                    {
                        if (g1 == null)
                        {
                            g1 = go;
                        }
                        else if (g2 == null)
                        {
                            g2 = go;
                        }

                    }
                    if (g1 != null && g2 == null)
                    {
                        Debug.Log("Active:" + itd.active + "\nGameobject (1):" + g1 + "\n Precent: " + itd.probabilityPercent + "\n Weight: " + itd.probabilityWeight);
                    }
                    else if (g1 != null && g2 != null)
                    {
                        Debug.Log("Active:" + itd.active + "\nGameobject (1):" + g1 + "\nGameobject (2): " + g2 + "\nPrecent: " + itd.probabilityPercent + "\nWeight: " + itd.probabilityWeight);
                    }
                    else
                    {
                        Debug.Log("Active:" + itd.active + "\nPrecent: " + itd.probabilityPercent + "\nWeight: " + itd.probabilityWeight);
                    }
                }
            }
            */
        #endregion
    }

    //Picks a number there is used to drop a item
    public ItemsToDrop PickLootDropItem()
    {
        //Test gameobjects
        GameObject g1;
        GameObject g2;
        //EndTest

        float pickedNumber = Random.Range(0, probabilityTotalWeight);

        Debug.Log(pickedNumber);
        
        // Find an item whose range contains pickedNumber
        foreach (ItemsToDrop itd in lootTable)
        {
            // If the picked number matches the item's range, return item
            if (pickedNumber > itd.probabilityRangeFrom && pickedNumber < itd.probabilityRangeTo)
            {
                //Test
                g1 = null;
                g2 = null;

                foreach (GameObject go in itd.items)
                {
                    if (g1 == null)
                    {
                        g1 = go;
                    }
                    else if (g2 == null)
                    {
                        g2 = go;
                    }
                }
                Debug.Log(g1 + "\n" + g2);
                //EndTest

                Debug.Log(itd.items.Length);

                return itd;
            }
        }

        // If item wasn't picked... Notify programmer via console and return the first item from the list
        Debug.LogError("Item couldn't be picked... Be sure that all of your active loot drop tables have assigned at least one item!");
        return lootTable[0];
    }

    /*
    void DropLootNearChest(int numItemsToDrop)
    {
        for (int i = 0; i < numItemsToDrop; i++)
        {
            ItemsToDrop selectedItem = PickLootDropItem();
            GameObject selectedItemGameObject = Instantiate(selectedItem.items);
            selectedItemGameObject.transform.position = new Vector2(i / 2f, 0f);
        }
    }
    */
}

