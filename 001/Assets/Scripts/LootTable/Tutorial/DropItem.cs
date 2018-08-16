using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item that can be picked by a LootDropTable.
/// </summary>
public abstract class DropItem<T>
{
    // Item it represents - usually GameObject, integer etc...
    public T item;

    // How many units the item takes - more units, higher chance of being picked
    public float probabilityWeight;

    // Displayed only as an information for the designer/programmer. Should not be set manually via inspector!    
    public float probabilityPercent;

    // These values are assigned via LootDropTable script. They represent from which number to which number if selected, the item will be picked.
    [HideInInspector]
    public float probabilityRangeFrom;
    [HideInInspector]
    public float probabilityRangeTo;
}
