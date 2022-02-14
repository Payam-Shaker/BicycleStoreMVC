CREATE TABLE [Production].[Category] (
    [Category_Id] INT IDENTITY(1,1) NOT NULL,
    [Category_Name] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY  ([Category_Id] ASC)
);