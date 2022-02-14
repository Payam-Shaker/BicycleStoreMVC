CREATE TABLE [Production].[Stock] (
    [Store_Id] INT,
    [Product_Id] INT,
    [Stock_Quantity] INT NULL,

    FOREIGN KEY (Store_Id) 
        REFERENCES Sales.Store (Store_Id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Product_Id) 
        REFERENCES Production.Product (Product_Id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
  
);