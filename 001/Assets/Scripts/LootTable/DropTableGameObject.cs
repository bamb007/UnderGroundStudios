using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When inheriting first we have to insert GenericLootDropItemGameObject instead of T and a GameObject instead of U
/// </summary>
[System.Serializable]
public class DropTableGameObject : LootTable<DropItemGameObject, GameObject> { }