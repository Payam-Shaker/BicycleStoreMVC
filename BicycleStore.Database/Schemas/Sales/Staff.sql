CREATE TABLE [Sales].[Staff]  (
    [Staff_Id] INT IDENTITY(1,1) NOT NULL,
    [Staff_FirstName] VARCHAR(255) NOT NULL,
    [Staff_LastName] VARCHAR(255) NOT NULL,
    [Staff_Email] VARCHAR(255) NOT NULL UNIQUE,
    [Staff_Phone] VARCHAR(50) NULL,
    [Store_Id] INT NOT NULL,
    [Staff_ManagerID] INT NULL,
/* 
Staff hierarchy specifies by the value in the [Staff_ManagerID] column.
If the value is 1 , then the staff has top access. */

    FOREIGN KEY (Store_Id) 
        REFERENCES Sales.Store (Store_Id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Staff_ManagerID) 
        REFERENCES Sales.Staff (Staff_Id) 
        ON DELETE NO ACTION ON UPDATE NO ACTION,

      CONSTRAINT [PK_Staff] PRIMARY KEY  ([Staff_Id] ASC)

);