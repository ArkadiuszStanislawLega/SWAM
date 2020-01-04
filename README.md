# SWAM
Warehouse management application designed for usage in DIY store created with WPF.

## Table of contents
* [General info](#general-info)
* [Setup](#setup)

## General info
This application is dedicated for retail of building materials. Based on user account type you get priviliges to get accesss to different parts of application to perform actions. It is designed for employees, so customer's actions in application are not taken.

## Setup
To run this project, you may need to customize Data Source of connectionString property in App.config, cause by default database runs on LocalDB.  

To create database go to Package Manager Console and type

```
update-database
```

This command will execute code in Up method of all existing migrations.
