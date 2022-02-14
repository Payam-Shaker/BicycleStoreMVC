--use [BicycleStore]
DECLARE @Cat_Id INT = 1;

--SET IDENTITY_INSERT [dbo].[Categories] ON
MERGE INTO [Production].[Category] AS TARGET 
USING (VALUES
(@Cat_Id ,'Race'),
(@Cat_Id+1, 'City'),
(@Cat_Id+2 ,'Hybrid')
)As source ([Category_Id], [Category_Name])
On target.[Category_Id] = source.[Category_Id]

----------------------------------------
--when changing  ONLY the name of current category 
---------------------------------------
WHEN MATCHED AND (target.[Category_Name] <> source.[Category_Name])
	THEN UPDATE SET
	[Category_Name] = source.[Category_Name]
----------------------------------------
--when Adding totally new category/eis 
---------------------------------------
WHEN NOT MATCHED BY TARGET
	THEN INSERT ([Category_Id], [Category_Name])
	VALUES ([Category_Id], [Category_Name])


WHEN NOT MATCHED BY source
	THEN DELETE;

--SET IDENTITY_INSERT [dbo].[Categories] OFF
GO