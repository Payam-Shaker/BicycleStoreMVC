CREATE TABLE [Sales].[Customer]  (
    [Customer_Id] INT IDENTITY(1,1) NOT NULL,
    [Customer_FirstName] VARCHAR(255) NOT NULL,
    [Customer_LastName] VARCHAR(255) NOT NULL,
    [Customer_Email] VARCHAR(255) NOT NULL ,
    [Customer_Phone] VARCHAR(50) NULL,
    [Customer_Street] VARCHAR(255) NULL,
    [Customer_City] VARCHAR(255) NULL,
    [Customer_ZipCode] VARCHAR(50) NULL,
    [Role] INT NULL,

  CONSTRAINT [PK_Customer] PRIMARY KEY  ([Customer_Id] ASC)

);
