--use [BicycleStore]
DECLARE @Cat_Id INT = 1;

SET IDENTITY_INSERT [Production].[Category] ON
MERGE INTO [Production].[Category] AS TARGET 
USING (VALUES
(@Cat_Id ,'Race'),
(@Cat_Id+1, 'City'),
(@Cat_Id+2 ,'Hybrid')
)As source ([CategoryID], [Category_Name])
On target.[CategoryID] = source.[CategoryID]

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
	THEN INSERT ([CategoryID], [Category_Name])
	VALUES ([CategoryID], [Category_Name])


WHEN NOT MATCHED BY source
	THEN DELETE;

SET IDENTITY_INSERT [Production].[Category] OFF
GO