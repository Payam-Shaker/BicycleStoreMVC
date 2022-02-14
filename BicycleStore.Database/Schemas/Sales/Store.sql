CREATE TABLE [Sales].[Store]  (
    [Store_Id] INT IDENTITY(1,1) NOT NULL,
    [Store_Name] VARCHAR(255) NULL,
    [Store_Phon] VARCHAR(50) NULL,
    [Store_Email] VARCHAR(255) NULL,
    [Store_Street] VARCHAR(255) NULL,
    [Store_City] VARCHAR(255) NULL,
    [Store_ZipCode] VARCHAR(50) NULL,

    CONSTRAINT [PK_Store] PRIMARY KEY  ([Store_Id] ASC)

);