/*******************************************************************
Name: Alex Wiley
Date: 12/7/2025
Assignment: SDC320 Week 4 Project – Database

Class to represent inventory record(s) from the inventory
table in the database. Note that these properties are public in this
case as this class is purely used to hold data from the Inventory
table

********************************************************************/
using System;
public class InventoryItem : IInventory
{
	public int ItemID {get; set;} //Unique identifier (will be PK for database)
	public string ItemName {get; set;} //Part name 
    public string ItemCategory {get; set;} //Category of item such as Laptop, Desktop, GPU, Power Supply etc.
    public int ItemQuantity {get; set;} //How many are on hand

    // public double ItemPrice {get; set;}  //The application is more so of an inventory manager, not to browse 
                                            // and shop... this may change through the life of the project
    public string ItemDescription {get; set;} //Brief description of the item
    public DateTime LastUpdated {get; set;} //Will call DateTime.Now to set the date and time of when the last time 
                                            //the inventory was updated
    public string UpdatedBy {get; set;} //Doing research to set a "logged in" user 

    public InventoryItem(int itemID, string name, string category, int quantity, string description, string updatedBy, DateTime lastUpdated)
    {
        ItemID = itemID;
        ItemName = name;
        ItemCategory = category;
        ItemQuantity = quantity;
        ItemDescription = description;
        LastUpdated = lastUpdated;
        UpdatedBy = updatedBy;
    }
    //SQLite should auto increment, so this second constructor will autogenerate the ItemID when an item
    //is inserted into the table.
    public InventoryItem(string name, string category, int quantity, string description)
    {
        ItemName = name;
        ItemCategory = category;
        ItemQuantity = quantity;
        ItemDescription = description;
    }

    public void UpdateQuantity(int qty) //method to adjust quantities in inventory
    {
        if (qty < 0)
        {
            throw new ArgumentException("Quantity cannot be a negative number.");
        }
                    
        ItemQuantity = qty;
    }

    public void IncreaseStock(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Increase amount cannot be negative.");
        }

        ItemQuantity += amount;
    }

    public void DecreaseStock(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Decrease amount cannot be negative.");
        }

        if (ItemQuantity - amount < 0)
        {
            throw new InvalidOperationException("Stock cannot go below zero.");
        }

        ItemQuantity -= amount;
    }
    public void UpdateDetails(string name, string category, string description) //method to update a name, description, or category for an item
    {
        ItemName = name;
        ItemCategory = category;
        ItemDescription = description;
    }

    public override string ToString() //used to output data pertaining to item info
    {
        return 
            $"[{ItemID}] {ItemName} - {ItemCategory} - Qty: {ItemQuantity}\n" +
            $"Last Updated: {LastUpdated} by {UpdatedBy}\n" +
            $"Description: {ItemDescription}";
    }

}