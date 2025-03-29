# Project Name

## Project Overview

This project is a .NET Framework MVC application designed to manage supplier access control and warehouse data.And has web api as well.

## Prerequisites

Ensure the following are installed on your system:

- Visual Studio 2019  
- .NET Framework 4.8
- SQL Server (LocalDB or full version)
 

## Installation Instructions

1. Clone the repository:
   ```bash
   git clone <https://github.com/saif364/InventoryManagementSystem.git>
   ```
2. Open the solution (.sln file) in Visual Studio.
3. Restore NuGet packages by right-clicking the solution and selecting "Restore NuGet Packages."

## Running the Project

1. Set the startup project in Visual Studio.
2. Update the database connection string in `Web.config` if needed.
3. Open Package Manager Console and run these three command:
   1. enable-migrations
   2. add-migration initialMigration
   3. update-database
   
4. Press F5 or click "Start Debugging".

## Usage Instructions

1. Access the application via `http://localhost:<port>`.

## Troubleshooting

- Ensure SQL Server is running.
- Verify connection strings.
 

## Contact Information

For assistance, contact [Mohmmad khalid saifullah] at [k1s1saif@gmail.com].

