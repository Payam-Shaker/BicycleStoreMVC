--use [BicycleStore]
DECLARE @Brd_Id INT = 1;

--SET IDENTITY_INSERT [Production].[Brand] ON
MERGE INTO [Production].[Brand] AS TARGET 
USING (VALUES
(@Brd_Id ,'Nishiki'),
(@Brd_Id+1, 'Trek'),
(@Brd_Id+2 ,'Monark'),
(@Brd_Id+3 ,'Bianchi'),
(@Brd_Id+4 ,'Moderna')
)As source ([Brand_Id], [Brand_Name])
On target.[Brand_Id] = source.[Brand_Id]

----------------------------------------
--when changing  ONLY the name of current category 
---------------------------------------
WHEN MATCHED AND (target.[Brand_Name] <> source.[Brand_Name])
	THEN UPDATE SET
	[Brand_Name] = source.[Brand_Name]
----------------------------------------
--when Adding totally new category/eis 
---------------------------------------
WHEN NOT MATCHED BY TARGET
	THEN INSERT ([Brand_Id], [Brand_Name])
	VALUES ([Brand_Id], [Brand_Name])


WHEN NOT MATCHED BY source
	THEN DELETE;

--SET IDENTITY_INSERT [Production].[Brand] OFF
GO