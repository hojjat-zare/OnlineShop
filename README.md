# OnlineShop
An online store whose products may be physical (requires shipping) or content(books, movies and ...).

There is no client software yet and currently only backend API has been developed.
a part of the base layer and services related to authentication have been developed.
This project is an example for clean architecture.

## How to run
Just run the project with visual studio or use below code:

```
dotnet run --project PublicApi
```

## Configuring SQL Server database

There are two Context in this project.
both context use same database connection string but you
can chage the connection string in appsettings.
This project use combination of databaseFirst and migrations, The reason is 
creating database with SQL is more robust and has more
features to increase performance but after creating database first state, there is need to add some changes to it during development so migration is necessary
The script for creating database is in <span>~/Eshop/Infrastructure/Data/DatabaseScripts/1- Create database.sql</span>

Note that this script uses T-SQL and is create for Microsoft SQL Server
if you want to use another database, modify the file especially commands to create
database files.

after creating first state of database, add connection string to appsetting in PublicApi project.
Then run these commands to modify database and
adding Identity tables to it:



```
cd .\Infrastructure\
dotnet restore
dotnet ef --startup-project ../PublicApi/ database update --context CatalogContext
dotnet ef --startup-project ../PublicApi/ database update --context AppIdentityDbContext
```

