# XamarinFormsSimpleSample
This is a bare minimum sample project in Xamarin Forms (but with complete features and efficient approach) on how to work with database (CRUD operation). This sample project is intended for newbies to get started in a correct way of implementing Xamarin Forms with database. 

The following lists the characteristics of this sample project:

- Build using Visual Studio 2017 (Ver. 15.9.11), and Xamarin Forms 3.4.0.1008975.
- Using .NET Standard 2.0 Class Library as the shared common codebase.
- Targeting Android and iOS only (should we support UWP? duh!).
- Using Entity Framework Core + SQLite as the database.
- Contains two entities (Department and Employee) with relationship (1 Department can have many Employees).
- The Main Page is implemented as Master Detail (menus opened with a hamburger button on the top left).
- Demonstrating proper UI binding with many data types such as string, int, decimal, DateTime, bool, and object reference (implemented with picker).
- Demonstrating proper Dependency Injection using Autofac (as the Database Service).
- Demonstrating efficient ViewModel and View abstraction pattern (for easy binding and wiring events).
- Demonstrating full CRUD (List of data, Create data, Update data, and Delete data) on both entities.
- Support resetting (clearing) all the data.
