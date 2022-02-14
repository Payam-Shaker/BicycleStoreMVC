--CREATE SCHEMA Production

CREATE TABLE [Production].[Brand] (
    [Brand_Id] INT IDENTITY(1,1) NOT NULL,
    [Brand_Name] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Brand] PRIMARY KEY  ([Brand_Id] ASC)
);