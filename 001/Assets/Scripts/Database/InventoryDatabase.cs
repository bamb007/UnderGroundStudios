using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class ItemInfo
{
    public int id;
    public string name;
    public string description;

    public static ItemInfo create(int id)
    {
        int t = (int)InventoryDatabase.ExecuteScalar<long>("SELECT ID FROM Items WHERE ID = " + id);
        string n = InventoryDatabase.ExecuteScalar<string>("SELECT Name FROM Items WHERE ID = " + id);
        string d = InventoryDatabase.ExecuteScalar<string>("SELECT Description FROM Items WHERE ID = " + id);

        return new ItemInfo() { id = t, name = n, description = d };
    }
}

[System.Serializable]
public class WeaponInfo
{
    public int id;
    public string name;
    public string description;
    public string weaponType;
    public string elementType;
    public int damage;
    public float firerate;
    public int range;

    public static WeaponInfo create(int id)
    {
        int t = (int)InventoryDatabase.ExecuteScalar<long>("SELECT ID FROM Weapons WHERE ID = " + id);
        string n = InventoryDatabase.ExecuteScalar<string>("SELECT Name FROM Weapons WHERE ID = " + id);
        string d = InventoryDatabase.ExecuteScalar<string>("SELECT Description FROM Weapons WHERE ID = " + id);
        string w = InventoryDatabase.ExecuteScalar<string>("SELECT WeaponType FROM Weapons WHERE ID = " + id);
        string e = InventoryDatabase.ExecuteScalar<string>("SELECT ElementType FROM Weapons WHERE ID = " + id);
        int dmg = (int)InventoryDatabase.ExecuteScalar<long>("SELECT Damage FROM Weapons WHERE ID = " + id);
        float f = (float)InventoryDatabase.ExecuteScalar<float>("SELECT FireRate FROM Weapons WHERE ID = " + id);
        int r = (int)InventoryDatabase.ExecuteScalar<long>("SELECT Range FROM Weapons WHERE ID = " + id);

        return new WeaponInfo() { id = t, name = n, description = d, weaponType = w, elementType = e, damage = dmg, firerate = f, range = r};
    }
}

[System.Serializable]
public class ResourcInfo
{
    public int id;
    public string name;
    public string description;


    public static ResourcInfo create(int id)
    {
        int t = (int)InventoryDatabase.ExecuteScalar<long>("SELECT ID FROM Resources WHERE ID = " + id);
        string n = InventoryDatabase.ExecuteScalar<string>("SELECT Name FROM Resources WHERE ID = " + id);
        string d = InventoryDatabase.ExecuteScalar<string>("SELECT Description FROM Resources WHERE ID = " + id);

        return new ResourcInfo() { id = t, name = n, description = d};
    }
}

[System.Serializable]
public class PerkInfo
{
    public int id;
    public string name;
    public string description;

    public static PerkInfo create(int id)
    {
        int t = (int)InventoryDatabase.ExecuteScalar<long>("SELECT ID FROM Perks WHERE ID = " + id);
        string n = InventoryDatabase.ExecuteScalar<string>("SELECT Name FROM Perks WHERE ID = " + id);
        string d = InventoryDatabase.ExecuteScalar<string>("SELECT Description FROM Perks WHERE ID = " + id);

        return new PerkInfo() { id = t, name = n, description = d};
    }
}

public class InventoryDatabase : MonoBehaviour
{
    private static string path;
    private static SqliteConnection connection;

    //InventoryTest

    public GameObject g;

    public GameObject achievement;

    public List<ItemInfo> itemsFromDatabase;

    public List<WeaponInfo> weaponsFromDatabase;

    public List<ResourcInfo> resourcesFromDatabase;

    public List<PerkInfo> perksFromDatabase;

    //EndTest

    void Awake()
    {
        //Test
        
        //EndTest

        path = Application.dataPath + "/StreamingAssets/Config.db";
        connection = new SqliteConnection(string.Format("Data Source={0};Version=3;", path));

        connection.Open();
    }

    private void Start()
    {
        CreateListsFromData();

        CreateIngameInventoryFromLists();

        SortAfterName();
    }

    #region ListFunctions

    private void DeleteDataFromAllLists()
    {
        itemsFromDatabase.Clear();
        perksFromDatabase.Clear();
        weaponsFromDatabase.Clear();
        resourcesFromDatabase.Clear();
    }

    private void SortAfterName()
    {
        foreach (WeaponInfo item in weaponsFromDatabase)
        {
            Debug.Log(item.name);
        }

        weaponsFromDatabase = weaponsFromDatabase.OrderBy(go => go.name).ToList();

        foreach (WeaponInfo item in weaponsFromDatabase)
        {
            Debug.Log(item.name);
        }
    }

    #endregion

    #region InventoryFunctions

    private void SetInventoryItems(string parent, ItemInfo inventoryInfo)
    {
        achievement = (GameObject)Instantiate(g);
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);

        achievement.transform.GetChild(0).GetComponent<Text>().text = inventoryInfo.id.ToString();
        achievement.transform.GetChild(1).GetComponent<Text>().text = inventoryInfo.name;
        achievement.transform.GetChild(2).GetComponent<Text>().text = inventoryInfo.description;
    }

    private void SetInventoryResources(string parent, ResourcInfo inventoryInfo)
    {
        achievement = (GameObject)Instantiate(g);
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);

        achievement.transform.GetChild(0).GetComponent<Text>().text = inventoryInfo.name;
        achievement.transform.GetChild(1).GetComponent<Text>().text = inventoryInfo.description;
        achievement.transform.GetChild(2).GetComponent<Text>().text = SaveDatabase.ExecuteScalar<long>("SELECT Amount FROM Resources WHERE Name = \"" + inventoryInfo.name + "\";").ToString();
    }

    private void SetInventoryWeapons(string parent, WeaponInfo inventoryInfo)
    {
        achievement = (GameObject)Instantiate(g);
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);

        achievement.transform.GetChild(0).GetComponent<Text>().text = inventoryInfo.name;
        achievement.transform.GetChild(1).GetComponent<Text>().text = inventoryInfo.description;
        achievement.transform.GetChild(2).GetComponent<Text>().text = inventoryInfo.weaponType;
    }

    private void SetInventoryPerks(string parent, PerkInfo inventoryInfo)
    {
        achievement = (GameObject)Instantiate(g);
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);

        achievement.transform.GetChild(0).GetComponent<Text>().text = inventoryInfo.name;
        achievement.transform.GetChild(1).GetComponent<Text>().text = inventoryInfo.description;
        achievement.transform.GetChild(2).GetComponent<Text>().text = inventoryInfo.id.ToString();
    }

    private void CreateIngameInventoryFromLists()
    {
        #region Sets up all lists to ingame images
        foreach (ItemInfo item in itemsFromDatabase)
        {
            SetInventoryItems("Items", item);
        }

        foreach (ResourcInfo item in resourcesFromDatabase)
        {
            SetInventoryResources("Resources", item);
        }

        foreach (WeaponInfo item in weaponsFromDatabase)
        {
            SetInventoryWeapons("Weapons", item);
        }

        foreach (PerkInfo item in perksFromDatabase)
        {
            SetInventoryPerks("Perks", item);
        }
        #endregion
    }

    #endregion

    #region Database functions
    public static int ExecuteNonQuery(string query)
    {
        using (SqliteCommand command = new SqliteCommand(query, connection))
        {
            return command.ExecuteNonQuery();
        }
    }

    public static SqliteDataReader ExecuteReader(string query)
    {
        using (SqliteCommand command = new SqliteCommand(query, connection))
        {
            return command.ExecuteReader(CommandBehavior.Default);
        }
    }
    
    public static T ExecuteScalar<T>(string query)
    {
        using (SqliteCommand command = new SqliteCommand(query, connection))
        {
            return (T)command.ExecuteScalar();
        }
    }
    public static int Count(string database)
    {
        int c = (int)InventoryDatabase.ExecuteScalar<long>("SELECT Count(*) From " + database);

        return c;
    }

    private void CreateListsFromData()
    {
        #region Takes all data from database and puts them in lists
        for (int i = 1; i <= Count("Items"); i++)
        {
            itemsFromDatabase.Add(ItemInfo.create(i));
        }

        for (int i = 1; i <= Count("Weapons"); i++)
        {
            weaponsFromDatabase.Add(WeaponInfo.create(i));
        }

        for (int i = 1; i <= Count("Resources"); i++)
        {
            resourcesFromDatabase.Add(ResourcInfo.create(i));
        }

        for (int i = 1; i <= Count("Perks"); i++)
        {
            perksFromDatabase.Add(PerkInfo.create(i));
        }
        #endregion
    }
    #endregion
}
