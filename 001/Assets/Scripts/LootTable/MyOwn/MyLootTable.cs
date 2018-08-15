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

    public bool permaUse;

    [HideInInspector]
    public float probabilityRangeFrom;
    [HideInInspector]
    public float probabilityRangeTo;
}

public class MyLootTable : MonoBehaviour
{
    [Header("DebugMode")]
    [SerializeField] private bool debugMode;
    [Space(10)]

    [Header("LootTable")]
    public List<ItemsToDrop> lootTable;

    [SerializeField] private int numOfItemsToDrop;
    [Space(10)]

    [Header("Spawn location offset")]

    [SerializeField] private float x;
    [SerializeField] private float y;
    [Space(10)]

    // Sum of all weights of items.
    float probabilityTotalWeight;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Used to testing
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnItem(numOfItemsToDrop);
        }
    }

    //Keeps it updated in inspector
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

            int deleteIfNoItemFound = 0;

            foreach (ItemsToDrop itd in lootTable)
            {
                if (itd.probabilityWeight < 0f || itd.active == false)
                {
                    // Prevent usage of negative weight.
                    if (debugMode)
                    {
                        Debug.Log("You can't have negative weight on an item or item inactive. Reseting item's weight to 0.");
                    }

                    itd.probabilityWeight = 0f;
                    deleteIfNoItemFound += 1;

                    if (deleteIfNoItemFound == lootTable.Count)
                    {
                        if (debugMode)
                        {
                            Debug.Log("Destroy the loottable script");
                        }
                        Destroy(this);
                    }
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
    private ItemsToDrop PickLootDropItem()
    {
        #region Test debug
        GameObject g1;
        GameObject g2;
        #endregion

        float pickedNumber = Random.Range(0, probabilityTotalWeight);

        Debug.Log(pickedNumber);

        if (debugMode)
        {
            Debug.Log("Number used to pick a item: " + pickedNumber);
        }

        // Find an item whose range contains pickedNumber
        foreach (ItemsToDrop itd in lootTable)
        {
            // If the picked number matches the item's range, return item
            if (pickedNumber > itd.probabilityRangeFrom && pickedNumber < itd.probabilityRangeTo && itd.active)
            {
                #region Test debug
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
                if (debugMode)
                {
                    Debug.Log(g1 + "\n" + g2);
                    Debug.Log(itd.items.Length);
                }

                #endregion

                return itd;
            }
        }

        // If item wasn't picked... Notify programmer via console and return the first item from the list
        if (debugMode)
        {
            Debug.LogError("Item couldn't be picked... Be sure that all of your active loot drop tables have assigned at least one item!");
        }

        return lootTable[0];
    }

    void SpawnItem(int numItemsToDrop)
    {
        for (int i = 0; i < numItemsToDrop; i++)
        {
            ItemsToDrop selectedDrop = PickLootDropItem();

            if (!selectedDrop.permaUse)
            {
                selectedDrop.active = false;
            }

            if (selectedDrop.items.Length > 0 && selectedDrop.items[0] != null)
            {
                for (int j = 0; j < selectedDrop.items.Length; j++)
                {
                    GameObject selectedItemGameObject = Instantiate(selectedDrop.items[j]);
                    selectedItemGameObject.transform.position = new Vector2(this.transform.position.x + x, this.transform.position.y + y);
                    
                    //Used to spawn objects in middle with distance
                    //selectedItemGameObject.transform.position = new Vector2((i + j) / 2f, 0);
                    
                }
            }
            else
            {
                if (debugMode)
                {
                    Debug.Log("Selected item does not contain a gameobject and does not work");
                }
            }

            if (!selectedDrop.permaUse)
            {
                selectedDrop.active = false;
                LootTableCheck();
            }
        }
    }
}

