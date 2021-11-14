--use [BicycleStore]
DECLARE @Cat_Id INT = 1;

SET IDENTITY_INSERT [dbo].[Categories] ON
MERGE INTO [dbo].[Categories] AS TARGET 
USING (VALUES
(@Cat_Id ,'Race'),
(@Cat_Id+1, 'City'),
(@Cat_Id+2 ,'Hybrid')
)As source ([CategoryID], [CategoryName])
On target.[CategoryID] = source.[CategoryID]

----------------------------------------
--when changing  ONLY the name of current category 
---------------------------------------
WHEN MATCHED AND (target.[CategoryName] <> source.[CategoryName])
	THEN UPDATE SET
	[CategoryName] = source.[CategoryName]
----------------------------------------
--when Adding totally new category/eis 
---------------------------------------
WHEN NOT MATCHED BY TARGET
	THEN INSERT ([CategoryID], [CategoryName])
	VALUES ([CategoryID], [CategoryName])


WHEN NOT MATCHED BY source
	THEN DELETE;

SET IDENTITY_INSERT [dbo].[Categories] OFF
GO