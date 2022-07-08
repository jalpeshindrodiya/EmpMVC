Create New Project Asp.net core Web App(Model-View-Controller), select framework .NET 6.0 
Right Click on the Project and go to Manage NuGet Packages - entity framework 
Create DataContext and set connection string for database.
Create model for employee and department
Click on Tools > Nugnet Package Manager>> Package Manager Console
Run Enable-Migrations, add-migration, update-database after that "Migrations" folder generated automatically and also create table in database.
Add new controller with the name "Employees" & "Departments" with crud operation method.
Change default "Home" controller to  "Employees" and hit F5 to run the sample.
Add Link "Employees" & "department" in Layout page.
Changes in controller,view and model as per requirements.