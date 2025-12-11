/***************************************************************

Name: Alessandro Wiley
Date: 12/10/2025
Class: SDC320 - Advanced Object-Oriented Programming using C# - Main Class Project

Class to handle menu routing options. This class gets the end user to 
the function of the program they need to navigate to and handles null entries
or non integer entries the end user may provide that could cause issues in the program.

*****************************************************************/

using System.Data.SQLite;

public class MenuUI
{

    private SQLiteConnection _conn;

    public MenuUI(SQLiteConnection conn)
    {
        _conn = conn;
    }
    public void ShowMainMenu() //Displays options to navigate to desired menu option
    {
        Console.WriteLine("Alessandro Wiley - Asset Inventory Management System");
        Console.WriteLine("Please select from the following options:");
        Console.WriteLine("1 - Add an item");
        Console.WriteLine("2 - Update an item");
        Console.WriteLine("3 - Remove an item");
        Console.WriteLine("4 - Search an item by ID");
        Console.WriteLine("5 - Search an item by category");
        Console.WriteLine("6 - See all items in inventory");
        Console.WriteLine("7 - Exit system");
    }

    public void Add() //Navigates to the menu option to add an item to the db
    {
        Console.Write("Item name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("This field cannot be null.");
            return;
        }

        Console.Write("Item category: ");
        string category = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(category))
        {
            Console.WriteLine("This field cannot be null.");
            return;
        }

        Console.Write("Quantity: ");
        string qtyInput = Console.ReadLine();

        if (!int.TryParse(qtyInput, out int qty) || qty < 0)
        {
            Console.WriteLine("You entered a non-integer value. Please enter a valid number");
            return;
            
        }

        Console.WriteLine($"Adding {qty} to inventory.");           

        Console.Write("Item description: ");
        string description = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("This field cannot be null.");
            return;
        }

        InventoryItem item = new InventoryItem(name, category, qty, description);

        InventoryManager.AddItem(_conn, item);
        Console.WriteLine($"{item} successfully added.");
    }

    public void Update() //Navigates to the menu option to modify an item in the db
    {
        Console.Write("Enter an ItemID to update: ");
        string idInput = Console.ReadLine();

        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }

        var existingItem = InventoryManager.SearchItemID(_conn, id);

        if (existingItem == null)
        {
            Console.WriteLine($"Item ID {id} does not exist.");
            return;
        }

        Console.WriteLine($"\nItem selected: {existingItem}");

        Console.Write("New Item Name (leave blank to keep current name): ");
        string name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
        {
            existingItem.ItemName = name;
        }

        Console.Write("New category (leave blank to keep current category): ");
        string category = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(category))
        {
            existingItem.ItemCategory = category;
        }

        Console.Write("New Quantity (leave blank to keep current quantity): ");
        string qtyInput = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(qtyInput))
        {
            
            if (!int.TryParse(qtyInput, out int qty) || qty < 0)
            {
                Console.WriteLine("You entered a non-integer value. Please enter a valid number");
                return;
                
            }
            existingItem.ItemQuantity = qty;
        }        

        Console.Write("New item description: (leave blank to keep current description): ");
        string description = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(description))
        {
            existingItem.ItemDescription = description;
        }

        InventoryManager.UpdateItem(_conn, existingItem);
        Console.WriteLine($"{existingItem} successfully updated.");
    }

    public void Remove() //Navigates to the menu option to remove an item in the db
    {
        Console.WriteLine("Enter ItemID you want to remove:");
        string idInput = Console.ReadLine();

        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var existingItem = InventoryManager.SearchItemID(_conn, id);

        if (existingItem == null)
        {
            Console.WriteLine($"Item ID {id} does not exist.");
            return;
        }

        Console.WriteLine($"\nItem selected: {existingItem}");
        Console.Write("Are you sure you want to delete this item? (Y/N): ");
        string confirm = Console.ReadLine()?.ToUpper();

        if (confirm == "Y")
        {
            InventoryManager.DeleteItem(_conn, id);
            Console.WriteLine($"Item ID {id} has been removed.");
        }
        else
        {
            Console.WriteLine("Delete operation cancelled.");
        }
    
    }

    public void ViewItem() //Navigate to the menu option to view an individual item in db
    {
        Console.WriteLine("Enter the Item ID you wish to review: ");
        string idInput = Console.ReadLine();

        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine($"Invalid input.");
            return;
        }

        var existingItem = InventoryManager.SearchItemID(_conn, id);

        if (existingItem == null)
        {
            Console.WriteLine($"Item ID {id} does not exist");
            return;
        }

        Console.WriteLine($"\nItem selected: {existingItem}");
    } 

    public void ViewItemCategory()
    {
        Console.WriteLine("Enter the Item Category you wish to view: ");
        string categoryInput = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(categoryInput))
        {
            Console.WriteLine("You did not enter any text into the search box.");
            return;
        }

        var categorySearch = InventoryManager.SearchCategory(_conn, categoryInput);

        if (categorySearch.Count == 0)
        {
            Console.WriteLine("No items found in this category.");
            return;
        }

        Console.WriteLine($"There were {categorySearch.Count} items matching category search:\n");
        
        foreach (var item in categorySearch)
        {
            Console.WriteLine(item);
            Console.WriteLine("------------------------");
        }
    }

    public void ViewAll()
    {
        var items = InventoryManager.GetAllInventory(_conn);

        if (items.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("The following items are in the inventory:\n");

        foreach (var item in items)
        {
            Console.WriteLine(item);
            Console.WriteLine("------------------------");
        }
    }


}