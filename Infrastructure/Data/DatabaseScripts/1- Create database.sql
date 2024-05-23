USE master
GO

CREATE DATABASE SimpleEshop
 ON  PRIMARY 
( NAME = N'SimpleEshop_Primary', FILENAME = N'F:\Database files\SimpleEshop_Primary.mdf' , SIZE = 128MB , FILEGROWTH = 64MB ), 
 FILEGROUP FGEntities 
( NAME = N'SimpleEshop_Entities', FILENAME = N'F:\Database files\SimpleEshop_Entities.ndf' , SIZE = 128MB , FILEGROWTH = 64MB )
 LOG ON 
( NAME = N'SimpleEshop_log', FILENAME = N'F:\Database files\SimpleEshop_log.ldf' , SIZE = 128MB , FILEGROWTH = 64MB )
GO
ALTER DATABASE SimpleEshop SET RECOVERY FULL 
GO




USE SimpleEshop
GO

CREATE TABLE dbo.CatalogBrands(
	Id int NOT NULL,
	Brand nvarchar(100) NOT NULL,
 CONSTRAINT PK_CatalogBrands PRIMARY KEY CLUSTERED (Id) ON FGEntities
 ) ON FGEntities
GO

CREATE TABLE dbo.CatalogTypes(
	Id int NOT NULL,
	Type nvarchar(100) NOT NULL,
 CONSTRAINT PK_CatalogTypes PRIMARY KEY CLUSTERED (Id) ON FGEntities
 ) ON FGEntities
GO

CREATE TABLE dbo.[Catalog](
	Id int NOT NULL,
	Name nvarchar(50) NOT NULL,
	Description nvarchar(1000) NULL,
	Price decimal(18, 2) NOT NULL,
	PictureUri nvarchar(500) NULL,
	IsAvailable bit NOT NULL,
	CatalogTypeId int NOT NULL,
	CatalogBrandId int NOT NULL,
	StockCount bigint NULL,
 CONSTRAINT PK_Catalog PRIMARY KEY CLUSTERED (Id) ON FGEntities,
 CONSTRAINT FK_Catalog_CatalogBrands_CatalogBrandId FOREIGN KEY(CatalogBrandId) REFERENCES dbo.CatalogBrands (Id) ON DELETE CASCADE,
 CONSTRAINT FK_Catalog_CatalogTypes_CatalogTypeId FOREIGN KEY(CatalogTypeId) REFERENCES dbo.CatalogTypes (Id) ON DELETE CASCADE
 ) ON FGEntities
GO

CREATE TABLE dbo.Baskets(
	Id int IDENTITY(1,1) NOT NULL,
	BuyerId nvarchar(256) NOT NULL,
	CONSTRAINT PK_Baskets PRIMARY KEY CLUSTERED (Id )ON FGEntities
) ON FGEntities
GO

CREATE TABLE dbo.BasketItems(
	Id int IDENTITY(1,1) NOT NULL,
	UnitPrice decimal(18, 2) NOT NULL,
	Quantity int NOT NULL,
	CatalogItemId int NOT NULL,
	BasketId int NOT NULL,
	CONSTRAINT PK_BasketItems PRIMARY KEY CLUSTERED (Id) ON FGEntities,
	CONSTRAINT FK_BasketItems_Baskets_BasketId FOREIGN KEY(BasketId) REFERENCES dbo.Baskets (Id) ON DELETE CASCADE,
	CONSTRAINT FK_BasketItems_Catalog_CatalogId FOREIGN KEY(CatalogItemId) REFERENCES dbo.[Catalog] (Id) ON DELETE CASCADE
) ON FGEntities
GO

CREATE NONCLUSTERED INDEX IX_BasketItems_BasketId ON dbo.BasketItems(BasketId)
GO

CREATE NONCLUSTERED INDEX IX_Catalog_CatalogBrandId ON dbo.Catalog (CatalogBrandId)
GO

CREATE NONCLUSTERED INDEX IX_Catalog_CatalogTypeId ON dbo.Catalog (CatalogTypeId) 
GO


CREATE TABLE dbo.Orders(
	Id int IDENTITY(1,1) NOT NULL,
	BuyerId nvarchar(256) NOT NULL,
	OrderDate datetime NOT NULL,
 CONSTRAINT PK_Orders PRIMARY KEY CLUSTERED (Id) ON FGEntities
 ) ON FGEntities
GO


CREATE TABLE dbo.OrderAddress(
	OrderId int NOT NULL,
	CompleteAddress nvarchar(300) NOT NULL,
	Street nvarchar(50) NOT NULL,
	City nvarchar(30) NOT NULL,
	State nvarchar(30) NOT NULL,
	Country nvarchar(30) NOT NULL,
	ZipCode nvarchar(18) NOT NULL,
	CONSTRAINT PK_OrderAddress PRIMARY KEY CLUSTERED (OrderId) ON FGENTITIES,
	CONSTRAINT FK_OrderAddress_Order_OrderId FOREIGN KEY(OrderId) REFERENCES dbo.Orders (Id)
) ON FGEntities
GO


CREATE TABLE dbo.OrderItems(
	Id int IDENTITY(1,1) NOT NULL,
	ItemOrdered_CatalogItemId int NULL,
	ItemOrdered_ProductName nvarchar(50) NULL,
	ItemOrdered_PictureUri nvarchar(4000) NULL,
	UnitPrice decimal(18, 2) NOT NULL,
	Units int NOT NULL,
	OrderId int NULL,
 CONSTRAINT PK_OrderItems PRIMARY KEY CLUSTERED (Id) ON FGEntities,
 CONSTRAINT FK_OrderItems_Orders_OrderId FOREIGN KEY(OrderId) REFERENCES dbo.Orders (Id)
 ) ON FGEntities
GO

CREATE NONCLUSTERED INDEX IX_OrderItems_OrderId ON dbo.OrderItems (OrderId)
GO
