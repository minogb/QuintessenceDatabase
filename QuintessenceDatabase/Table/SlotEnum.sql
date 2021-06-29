CREATE TABLE [dbo].[SlotEnum]
(
	[Slot] INT NOT NULL, 
	[ParentSlot] INT NULL , 
    [Name] NCHAR(10) PRIMARY KEY,
	UNIQUE([Slot],[ParentSlot])
)
