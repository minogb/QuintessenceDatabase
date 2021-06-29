CREATE TABLE [dbo].[Stack]
(
	[StackID] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [ParentStackID] INT, 
    [ItemID] INT NOT NULL,   
    [ParentID] INT,
    [QValue] INT NOT NULL,
     FOREIGN KEY ([ItemID]) REFERENCES [Item]([ItemID]),
     FOREIGN KEY ([ParentStackID]) REFERENCES [Stack]([StackID])
)
