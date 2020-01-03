# SWAM
Warehouse management application designed for usage in DIY store created with WPF.

## Table of contents
* [Setup](#setup)

## Setup
To run this project, you may need to customize Data Source of connectionString property in App.config, cause by default database runs on LocalDB.  

To create database go to Package Manager Console and type

```
update-database
```

This command will execute code in Up method of all existing migrations.
