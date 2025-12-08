/*******************************************************************
Name: Alex Wiley
Date: 12/7/2025
Assignment: SDC320 Week 4 Project – Database

Interface that tdefines required members for inventory objects.

********************************************************************/
public interface IInventory
{
    int ItemID{get; set;}
    string ItemName {get; set;}
    int ItemQuantity {get; set;}

    string InventoryStatus() //Displays a message if inventory is well stocked or if items need to be ordered
    {
        if (ItemQuantity == 0)
            return $"{ItemName} is out of stock.";
        else if (ItemQuantity < 7)
            return $"{ItemName} stock is running low. {ItemQuantity} remaining. Place order to restock.";
        else
            return $"{ItemName} has {ItemQuantity} remaining.";
    }
    string ToString();
    
}