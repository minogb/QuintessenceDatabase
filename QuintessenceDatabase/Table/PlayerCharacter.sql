CREATE TABLE [dbo].[PlayerCharacter]
(
	[PlayerCharacterID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [X] FLOAT NOT NULL, 
    [Y] FLOAT NOT NULL, 
    [Z] FLOAT NOT NULL, 
    [HelmID] INT NULL, 
    [ChestID] INT NULL, 
    [LeggingID] INT NULL, 
    [BootsID] INT NULL, 
    [GlovesID] INT NULL, 
    [Ring] INT NULL, 
    [Neckless] INT NULL, 
    [InventorySize] INT NOT NULL DEFAULT 30, 
    [BankSize] INT NOT NULL DEFAULT 300
)
