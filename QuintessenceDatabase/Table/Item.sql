CREATE TABLE [dbo].[Item]
(
	[ItemID] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Name] NCHAR(10) NOT NULL,
    [Description] NVARCHAR(200) NOT NULL,
    [StackSize] INT NULL
)
