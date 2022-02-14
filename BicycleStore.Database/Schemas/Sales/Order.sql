CREATE TABLE [Sales].[Order]  (
    [Order_Id] INT IDENTITY(1,1) NOT NULL,
	/* [Order_Status]: 1 = Pending; 2 = Processing; 
	3 = Rejected; 4 = Completed */
    [Order_Status] SMALLINT NOT NULL,
    [Order_Date] DATE NOT NULL,
    [Order_Required_Date] DATE NOT NULL,
    [Order_Shipped_Date] DATE NULL,
    [Customer_Id] INT NULL,
    [Staff_Id] INT NOT NULL,
    [Store_Id] INT NOT NULL,

    FOREIGN KEY (Store_Id) 
        REFERENCES Sales.Store (Store_Id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Staff_Id) 
        REFERENCES Sales.Staff (Staff_Id) 
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY (Customer_Id) 
        REFERENCES Sales.Customer (Customer_Id) 
        ON DELETE NO ACTION ON UPDATE NO ACTION,

      CONSTRAINT [PK_Order] PRIMARY KEY ([Order_Id] ASC)

);