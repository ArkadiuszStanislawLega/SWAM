# SWAM
Warehouse management application designed for usage in DIY store created with WPF.

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [User types and roles](#user-types-and-roles)

## General info
This application is dedicated for retail of building materials. Based on user account type you get priviliges to get accesss to different parts of application to perform actions. It is designed for employees, so customer's actions in application are not taken.

## Technologies
Project is created with:
* Windows Presentation Foundation
* .NET Framework V4.6.1
* EntityFramework V6.2.0

## Setup
To run this project, you may need to customize Data Source of connectionString property in App.config, cause by default database runs on LocalDB.  

To create database go to Package Manager Console and type

```
update-database
```

This command will execute code in Up method of all existing migrations.

## User types and roles
In application are six types of user:
* **Administrator** - person who has the authority to manage the user database and warehouses,
* **Warehouseman** - person who accepts the products to the warehouse,
* **Seller** - person who can sell our products do the clients,
* **Manager** - person who creates orders for the warehouse. 
* **Owner** - person who is the owner of retail business.
* **Programmer** - person who has permission to all pages in appliaction.