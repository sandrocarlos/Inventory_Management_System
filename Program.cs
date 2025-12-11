/***************************************************************

Name: Alessandro Wiley
Date: 12/9/2025
Class: SDC320 - Advanced Object-Oriented Programming using C# - Main Class Project

Main application class.

*****************************************************************/

using System.Data.SQLite;

public class ClassProject
{
    public static void Main(string[] args)
    {
        SQLiteConnection conn = new SQLiteConnection("Data Source = inventory.db");
        conn.Open();

        InventoryManager.CreateTable(conn);

        MenuUI menu = new MenuUI(conn);

        bool running = true;

        while (running)
        {
            menu.ShowMainMenu();
            Console.Write("\nYour menu selection: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    menu.Add();
                    break;
                case "2":
                    menu.Update();
                    break;
                case "3":
                    menu.Remove();
                    break;
                case "4":
                    menu.ViewItem();
                    break;
                case "5":
                    menu.ViewItemCategory();
                    break;
                case "6":
                    menu.ViewAll();
                    break;
                case "7":
                    running = false;
                    Console.WriteLine("You chose to exit the system. Farewell.");
                    continue;
                default:
                    Console.WriteLine("You made an invalid selection.");
                    break;
            }

            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
        }
        
        conn.Close();
    }
}