## 🗂️ Asset Inventory Management System
**📌 Project Overview**
___________________________________________
The Asset Inventory Management System is a C# console-based application designed to manage physical or digital assets using a persistent SQLite database. The application provides full Create, Read, Update, and Delete (CRUD) functionality, allowing users to efficiently track inventory items, update quantities, search by category, and view detailed item records.

This project demonstrates the application of object-oriented programming principles, database integration, data validation, and clean separation of concerns. It was developed as a capstone-style project intended to showcase real-world software design patterns and best practices suitable for entry-level software development and database-focused roles.
___________________________________________

**🎯 Project Objectives**

- Design a modular, object-oriented C# application

- Integrate a relational database (SQLite) for persistent storage

- Implement safe and reliable CRUD operations

- Apply input validation and exception handling

- Demonstrate clean console-based user interaction

- Produce a project suitable for a professional portfolio


**🧱 Technologies Used**

- Language: C#

- Framework: .NET

- Database: SQLite

- Data Access: System.Data.SQLite

- Version Control: Git & GitHub

- Development Environment: Visual Studio Code

**⚙️ Core Features**
✅ Inventory Management

- Add new inventory items with name, category, description, and quantity

- Automatically timestamps inventory updates

- Supports auditing via an UpdatedBy field

**🔄 Update Items**

- Modify existing inventory records

- Supports partial updates (fields may be left blank to retain current values)

- Automatically updates last-modified timestamps

**❌ Remove Items**

- Delete inventory items by unique Item ID

- Includes confirmation prompt to prevent accidental deletion

**🔍 Search Functionality**

- Search by Item ID to view full item details

- Search by Item Category using a case-insensitive wildcard search

    - Example: searching for "lap" will return "Laptop"

**📋 View Inventory**

- View all inventory items sorted by category

- Clean, readable output using a custom ToString() override

**🧠 Object-Oriented Design**

This project follows strong object-oriented principles:

- Encapsulation: Business logic is separated from user interface logic

- Abstraction: Inventory data is represented using a dedicated InventoryItem model

Separation of Concerns:

- InventoryManager handles all database interactions

- MenuUI manages user interaction and validation

- Program.cs controls application flow

**🛡️ Data Safety & Validation**

- Parameterized SQL queries prevent SQL injection

- User input is validated for nulls, empty strings, and invalid numeric values

- Database reads safely handle null values using IsDBNull()

- Graceful error handling prevents runtime crashes


📂 Repository Structure

├── InventoryItem.cs

├── InventoryManager.cs

├── MenuUI.cs

├── Program.cs

├── inventory.db

├── README.md

**🏁 Project Status**

Phase #3 — Complete

This project is complete and fully functional. Future enhancements may include:

- User authentication

- Inventory import/export (CSV)

- GUI or web-based interface

👤 Author

Alessandro Wiley
Software Development Student
