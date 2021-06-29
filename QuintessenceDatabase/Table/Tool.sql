CREATE TABLE [dbo].[Tool]
(
	[ToolID] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ItemID] INT NOT NULL,
	[ToolLevel] INT NOT NULL,
	[ToolType] INT NOT NULL,
	[Efficiency] float NOT NULL,
    [IsModifier] BIT NOT NULL,
     FOREIGN KEY ([ItemID]) REFERENCES [Item]([ItemID]),
)
