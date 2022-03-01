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

Registration and login system in BicycleStore application is
managed using some simple methods which goes through methods in UserController.cs. The user data is saved to 
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


