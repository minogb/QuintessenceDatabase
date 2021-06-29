CREATE TABLE [dbo].[Inventory]
(
	[PlayerID] INT NOT NULL, 
    [Slot] INT NOT NULL, 
    [StackID] INT NOT NULL,
     PRIMARY KEY([PlayerID],[Slot]),
     UNIQUE([PlayerID],[Slot]),
     FOREIGN KEY ([PlayerID]) REFERENCES [PlayerCharacter]([PlayerCharacterID])
)
