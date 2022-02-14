CREATE TABLE [Production].[Product] (
    [Product_Id] INT IDENTITY(1,1) NOT NULL,
    [Category_Id] INT NOT NULL,
    [Brand_Id] INT  NOT NULL,
    [Product_Name] VARCHAR(50) NOT NULL,
    [Product_Year] SMALLINT NOT NULL,
    [Product_Price] DECIMAL(10,2) NOT NULL,


    FOREIGN KEY (Category_Id) 
        REFERENCES Production.Category (Category_Id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (Brand_Id) 
        REFERENCES Production.Brand (Brand_Id) 
        ON DELETE CASCADE ON UPDATE CASCADE,

    CONSTRAINT [PK_Product] PRIMARY KEY  ([Product_Id] ASC)
);