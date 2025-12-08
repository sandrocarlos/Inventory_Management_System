/*******************************************************************
Name: Alex Wiley
Date: 12/7/2025
Assignment: SDC320 Week 4 Project – Database

Class to handle databse interactions with a SQLite database. The
connect method will either connect to an existing database or
create the database if the database doesn't exist.

********************************************************************/
using System.Data.SQLite;


public class InventoryManager
{

    public static void CreateTable(SQLiteConnection conn)
    {
        string sql = 
            "CREATE TABLE IF NOT EXISTS Inventory (\n"
            + "     ItemID              INTEGER PRIMARY KEY\n"
            + "     ,ItemName           VARCHAR(150)\n"
            + "     ,ItemCategory       VARCHAR(150)\n"
            + "     ,ItemDescription    VARCHAR(200)\n"
            + "     ,ItemQuantity       INTEGER\n"
            + "     ,LastUpdated        TIMESTAMP\n"
            + "     ,UpdatedBy          VARCHAR(50))";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();

    }

    //Add item to database without risking break with a random or stray single quotation mark in a string field like
    //name, description, or category. 
    public static void AddItem(SQLiteConnection conn, InventoryItem i)
    {
        string sql = 
            "INSERT INTO Inventory(ItemName, ItemCategory, ItemDescription, ItemQuantity, UpdatedBy, LastUpdated) "
            + "VALUES(@name, @category, @description, @quantity, @updatedBy, @lastUpdated)";
            
        using (SQLiteCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@name", i.ItemName);
            cmd.Parameters.AddWithValue("@category", i.ItemCategory);
            cmd.Parameters.AddWithValue("@description", i.ItemDescription);
            cmd.Parameters.AddWithValue("@quantity", i.ItemQuantity);
            cmd.Parameters.AddWithValue("@updatedBy", i.UpdatedBy);
            cmd.Parameters.AddWithValue("@lastUpdated", DateTime.Now);
            cmd.ExecuteNonQuery();
        }
    }

    public static void UpdateItem(SQLiteConnection conn, InventoryItem i)
    {
        string sql = 
            "UPDATE Inventory SET "
            + "ItemName = @name, "
            + "ItemCategory = @category, "
            + "ItemDescription = @description, "
            + "ItemQuantity = @quantity, "
            + "UpdatedBy = @updatedBy, "
            + "LastUpdated = @lastUpdated "
            + "WHERE ItemID = @id";

        using (SQLiteCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@name", i.ItemName);
            cmd.Parameters.AddWithValue("@category", i.ItemCategory);
            cmd.Parameters.AddWithValue("@description", i.ItemDescription);
            cmd.Parameters.AddWithValue("@quantity", i.ItemQuantity);
            cmd.Parameters.AddWithValue("@updatedBy", i.UpdatedBy);
            cmd.Parameters.AddWithValue("@lastUpdated", DateTime.Now);
            cmd.Parameters.AddWithValue("@id", i.ItemID);
            cmd.ExecuteNonQuery();
        }
    }

    public static void DeleteItem(SQLiteConnection conn, int itemID)
    {
        string sql = "DELETE from Inventory WHERE ItemID = @id";

        using (SQLiteCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", itemID);
            cmd.ExecuteNonQuery();
        }
    }

    public static List<InventoryItem> GetAllInventory(SQLiteConnection conn)
    {
        List<InventoryItem> items = new List<InventoryItem>();
        string sql = "SELECT * FROM Inventory";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            items.Add(new InventoryItem(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetInt32(4),
                rdr.GetString(3),
                rdr.GetString(6),            
                rdr.GetDateTime(5)
            ));
        }

        return items;
    }

    public static InventoryItem SearchItemID(SQLiteConnection conn, int itemID)
    {
        string sql = "SELECT * FROM Inventory WHERE ItemID = @id";

        using (SQLiteCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", itemID);

            using (SQLiteDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    return new InventoryItem(
                        rdr.GetInt32(0),
                        rdr.GetString(1),
                        rdr.GetString(2),
                        rdr.GetInt32(4),
                        rdr.GetString(3),
                        rdr.GetString(6),            
                        rdr.GetDateTime(5)
                    );
                }
                
                return null;
            }
        }
    }

    public static List<InventoryItem> SearchCategory(SQLiteConnection conn, string category)
    {
        List<InventoryItem> searchCategory = new List<InventoryItem>();
        string sql = "SELECT * FROM Inventory WHERE ItemCategory = @category";

        using (SQLiteCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@category", category);

            using (SQLiteDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    searchCategory.Add(new InventoryItem(
                        rdr.GetInt32(0),
                        rdr.GetString(1),
                        rdr.GetString(2),
                        rdr.GetInt32(4),
                        rdr.GetString(3),
                        rdr.GetString(6),            
                        rdr.GetDateTime(5)
                    ));
                }
            }
        }
        return searchCategory;
    }     
}