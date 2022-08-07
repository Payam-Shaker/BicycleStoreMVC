# BicycleStoreMVC

BicycleStore is an e-commerce where bikes of different brands and models are sold. This
platform is built as a MVC application with .NET Core 3.1 (long-term support), connected to
MsSQL database where products, orders, users and other information are stored.
The development of this project was done during different phases of planning, researching,
and eventually coding, with testing the different functionalities of the application in horizon.
The need for sophisticated e- commerce where specialized products are sold was the main
drive behind developing this system.

## Description

This project has been developed using Visual Studio 2019, community
edition, consists of 4 different projects: A class library to store Models and Data Transfarable
Objects (DTOs) in, a test project which is ongoing and will grow as the application scales up,
a web project where views, controlled and database context is defined and a database catalog
where table scripts and future stored procedures are stored. The database is currently divided to two
different schemas, namely Production and Sales. 

## Setup
To run this project locally, a new database can be created by SSMS whose connection string can be pasted into appsettings.json of BicycleStoreMVC project. 
This solution comes with a class library, BicycleStore.Data, where tabel relations are defined in a DbContext. After passing in the new connection string 
entity framework commands, Add-migration and then Update-Database, can be run via PMC, while the default project is set on BicycleStore.Data. Alternatively, one can 
create and populate tables with scripts that are available in BicycleStore.Database catalogue. 
By running the application on IIS Express the created tables will be populated with some test data that are available, and can be modified, in SeedData.cs in BicycleStore.Data.

Registration and login system in BicycleStore application is
managed using some simple methods from a UserService which has been injected into the UserController.cs. The user data is saved to 
[Sales].[Customer] table by generating password hash and password salt for the password
they type into the registration form.

```cs
public IActionResult Register(CustomerDto customerDto)
{
if (ModelState.IsValid)
{
try
{
//create password salt passwordhash
byte[] passwordHash, passwordSalt;
CreatePasswordHash(customerDto.Password, out passwordHash, out passwordSalt);
…

private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
{
if (password == null) throw new ArgumentNullException("password");
if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or
whitespace only string.", "password");
using (var hmac = new System.Security.Cryptography.HMACSHA512())
{
passwordSalt = hmac.Key;
passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
}
}

```

### Installing

This project can be cloned from this link:
https://github.com/Payam-Shaker/BicycleStoreMVC.git


