--use [BicycleStore]
DECLARE @Brd_Id INT = 1;

SET IDENTITY_INSERT [dbo].[Brands] ON
MERGE INTO [dbo].[Brands] AS TARGET 
USING (VALUES
(@Brd_Id ,'Nishiki'),
(@Brd_Id+1, 'Trek'),
(@Brd_Id+2 ,'Monark'),
(@Brd_Id+3 ,'Bianchi')
)As source ([BrandID], [BrandName])
On target.[BrandID] = source.[BrandID]

----------------------------------------
--when changing  ONLY the name of current category 
---------------------------------------
WHEN MATCHED AND (target.[BrandName] <> source.[BrandName])
	THEN UPDATE SET
	[BrandName] = source.[BrandName]
----------------------------------------
--when Adding totally new category/eis 
---------------------------------------
WHEN NOT MATCHED BY TARGET
	THEN INSERT ([BrandID], [BrandName])
	VALUES ([BrandID], [BrandName])


WHEN NOT MATCHED BY source
	THEN DELETE;

SET IDENTITY_INSERT [dbo].[Brands] OFF
GO