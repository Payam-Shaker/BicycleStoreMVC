CREATE TABLE [Sales].[Order_Item]  (
    [Order_Id] INT,
    [Item_Id] INT,
    [Product_Id] INT NOT NULL,
    [Item_Quantity] INT NOT NULL,
    [List_Price] DECIMAL(10,2) NOT NULL,
    [Discount] DECIMAL(4,2) NOT NULL DEFAULT 0,

    PRIMARY KEY (Order_Id, Item_Id),
    FOREIGN KEY (Order_Id) 
        REFERENCES Sales.[Order](Order_Id) 
        ON DELETE CASCADE ON UPDATE CASCADE,

    FOREIGN KEY (Product_Id) 
        REFERENCES Production.Product (Product_Id) 
        ON DELETE CASCADE ON UPDATE CASCADE
);