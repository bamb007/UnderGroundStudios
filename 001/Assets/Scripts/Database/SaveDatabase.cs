using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class SaveDatabase : MonoBehaviour
{
    private static SqliteConnection connection;

    // Use this for initialization
    void Awake()
    {
        string savePath = Application.dataPath + "/Saves/save.db";

        if (!File.Exists(savePath))
        {
            File.Copy(Application.dataPath + "/StreamingAssets/Structure.db", savePath);
        }

        connection = new SqliteConnection(string.Format("Data Source={0};Version=3;", savePath));

        connection.Open();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region DatabaseFunctions
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

    public static T ExecuteScalar<T>(string query) where T: new()
    {
        using (SqliteCommand command = new SqliteCommand(query, connection))
        {
            object result = command.ExecuteScalar();
            T converted = new T();

            try
            {
                converted = (T)result;
            }
            catch (System.Exception ex)
            {
                Debug.Log("Query: " + query);
                Debug.Log(ex.Message);
            }

            return converted;
        }
    }
    public static int Count(string database)
    {
        int c = (int)InventoryDatabase.ExecuteScalar<long>("SELECT Count(*) From " + database);

        return c;
    }
    #endregion
}
