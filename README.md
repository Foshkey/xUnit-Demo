# xUnit Demo

This is a project meant to demonstrate unit testing with xUnit. The app is meant to represent a bar, in which you place an order, and the bartender retrieves drinks for you.

This app is broken out into the following components:

* **Bar.CLI** - .NET Core console application that is responsible for the command-line interface and executing the app.
* **Bar.Tender** - .NET Standard library for the main parsing & fetching logic of the app.
* **Bar.Inventory** - .NET Standard library for providing the current inventory of drinks.
* **Bar.Models** - .NET Standard library for providing common models between components.

Each component has a respective test project in the Tests folder, with many different example use cases of xUnit.

## How to get started

You can run the command-line interface a number of different ways. Compile and execute Bar.CLI in visual studio, or if you have the [dotnet sdk](https://dot.net/) installed (.NET Core v2.1+), you can execute a `dotnet run` command targeting the Bar.CLI project. (e.g. `dotnet run --project Source/Bar.CLI`).

If you have the .NET Core SDK, open a command line at this directory (Same as Bar.sln), and enter this command:

```
dotnet run --project Source/Bar.CLI
```

This will build and execute Bar.CLI.

For running unit tests, enter this command:

```
dotnet test Tests
```

This will execute all unit tests within the Tests folder (using Tests.sln).

## Sample app execution

User input is denoted by the `>` symbol at the start of the line:

```
> dotnet run --project Source/Bar.CLI
Welcome to the xBar! What would you like to order?
> one beer
Coming right up!

Here is a Bonfire Brown!
An American brown ale by Saugatuck Brewing Co.
It costs $5.50.

Would you like anything else?
> no

Here is your tab.
Order 1 total: $5.50.
Grand Total: $5.50.
Have a nice day!

Press any key to continue...
```

## Sample test execution

User input is denoted by the `>` symbol at the start of the line:

```
> dotnet test Tests
Build started, please wait...
Build started, please wait...
Build started, please wait...
Build started, please wait...
Build completed.

Test run for C:\<path>\Bar.Models.Tests.dll(.NETCoreApp,Version=v2.1)
Microsoft (R) Test Execution Command Line Tool Version 15.9.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

Total tests: 5. Passed: 5. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 1.8353 Seconds
Build completed.

Test run for C:\<path>\Bar.Inventory.Tests.dll(.NETCoreApp,Version=v2.1)
Microsoft (R) Test Execution Command Line Tool Version 15.9.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

Total tests: 21. Passed: 21. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 1.6772 Seconds
Build completed.

Test run for C:\<path>\Bar.Tender.Tests.dll(.NETCoreApp,Version=v2.1)
Microsoft (R) Test Execution Command Line Tool Version 15.9.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Build completed.

Test run for C:\<path>\Bar.CLI.Tests.dll(.NETCoreApp,Version=v2.1)
Starting test execution, please wait...
Microsoft (R) Test Execution Command Line Tool Version 15.9.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

Total tests: 53. Passed: 53. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 1.6953 Seconds

Total tests: 24. Passed: 24. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 1.7431 Seconds
```