CREATE TABLE [dbo].[Equipment]
(
	[ItemID] INT NOT NULL PRIMARY KEY,
	[Slot] INT NOT NULL,
	[ParentSlot] INT NULL,
	[AvailableSlots] INT NULL,
     FOREIGN KEY ([ItemID]) REFERENCES [Item]([ItemID])
)
