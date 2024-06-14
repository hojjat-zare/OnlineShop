# OnlineShop
An online store whose products may be physical (requires shipping) or content(books, movies and ...).

There is no client software yet and currently only backend API has been developed.
a part of the base layer and services related to authentication have been developed.
This project is an example for clean architecture.

To build and run project dotnet 8 is required.

## Configuring database

There are two Context in this project.
both context use same database connection string but you
can chage the connection string in appsettings.
This project use combination of databaseFirst and migrations, The reason is 
creating database with SQL is more robust and has more
features to increase performance but after creating database first state, there is need to add some changes to it during development so migration is necessary


### Pure code first methodology
After all if you prefer code first methodology, delete migrations folder from ~/Eshop/Infrastructure/Data/ and create migrations from scratch:

```
dotnet ef --startup-project ../PublicApi/  migrations add Initialize --context CatalogContext --output-dir Data/Migrations/CatalogContextMigrations
dotnet ef --startup-project ../PublicApi/  migrations add Initialize --context AppIdentityDbContext --output-dir Data/Migrations/AppIdentityContextMigrations
```

### DB first combination with migrations
The script for creating database is in <span>~/Eshop/Infrastructure/Data/DatabaseScripts/1- Create database.sql</span>
Note that this script uses T-SQL and is created for Microsoft SQL Server
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

## Email Sender Service
After registeration application sends an email containing verification link to user. add your verificatoin email address and password
to **ApplicationCore.Constants.EmailSendingConstants** or add them to environment variables.


## How to run
Just run the project with visual studio or use below code:

```
dotnet run --project PublicApi
```
