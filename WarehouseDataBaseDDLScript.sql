CREATE DATABASE WarehouseDataBase;

GO

use WarehouseDataBase;

GO
 
DROP TABLE IF EXISTS tblProduct;


CREATE TABLE tblProduct (
    ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    ProductName nvarchar(50),
    ProductNumber nvarchar(50),
    Amount int,
    Price nvarchar(50),
    InStock bit	
);

GO

DROP VIEW IF EXISTS vwProduct;

GO

CREATE VIEW vwProduct
AS

SELECT        dbo.tblProduct.*
FROM            dbo.tblProduct
 
