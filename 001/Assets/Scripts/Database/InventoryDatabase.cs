using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

[System.Serializable]
public class items
{
    public int id;
    public string name;
    public string description;

    public static items create(int id)
    {
        int t = (int)InventoryDatabase.ExecuteScalar<long>("SELECT ID FROM Items WHERE ID = " + id);
        string n = InventoryDatabase.ExecuteScalar<string>("SELECT Name FROM Items WHERE ID = " + id);
        string d = InventoryDatabase.ExecuteScalar<string>("SELECT Description FROM Items WHERE ID = " + id);

        return new items() { id = t, name = n, description = d };
    }

}
public class InventoryDatabase : MonoBehaviour
{
    private static string path;
    private static SqliteConnection connection;

    //InventoryTest

    public GameObject g;

    public GameObject achievement;

    public List<items> itemsFromDatabase;

    //EndTest

    void Awake()
    {
        //Test
        
        //EndTest

        path = Application.dataPath + "/StreamingAssets/items.db";
        connection = new SqliteConnection(string.Format("Data Source={0};Version=3;", path));

        connection.Open();
    }

    private void Start()
    {
        items ites = items.create(1);
        
        achievement = (GameObject)Instantiate(g);
        achievement.transform.SetParent(GameObject.Find("Test").transform);
        achievement.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        achievement.transform.GetChild(0).GetComponent<Text>().text = ites.id.ToString();
        achievement.transform.GetChild(1).GetComponent<Text>().text = ites.name;
        achievement.transform.GetChild(2).GetComponent<Text>().text = ites.description;
    }

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
}
