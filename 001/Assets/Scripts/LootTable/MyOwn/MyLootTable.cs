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
    private ItemsToDrop test;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}
