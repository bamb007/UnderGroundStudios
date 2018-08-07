using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
/// <summary>
/// The most simple treasure chest script ever made :P basically on game start it spawns items in a straight line.
/// </summary>
[System.Serializable]
public class TreasureChest : MonoBehaviour
{

    // Loot drop table that contains items that can spawn
    public DropTableGameObject lootDropTable;

    // How many items treasure will spawn
    public int numItemsToDrop;

    // Runs when we start our game
    public void Start()
    {

        // Spawn objects in a straight line
        DropLootNearChest(numItemsToDrop);

    }

    void OnValidate()
    {

        // Validate table and notify the programmer / designer if something went wrong.
        lootDropTable.ValidateTable();

    }

    /// <summary>
    /// Spawning objects in horizontal line
    /// </summary>
    /// <param name="numItemsToDrop"></param>
    void DropLootNearChest(int numItemsToDrop)
    {
        for (int i = 0; i < numItemsToDrop; i++)
        {
            DropItemGameObject selectedItem = lootDropTable.PickLootDropItem();
            GameObject selectedItemGameObject = Instantiate(selectedItem.item);
            selectedItemGameObject.transform.position = new Vector2(i / 2f, 0f);
        }
    }
}