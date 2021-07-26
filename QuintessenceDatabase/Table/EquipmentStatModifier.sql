CREATE TABLE [dbo].[EquipmentStatModifier]
(
	[StatID] INT NOT NULL, 
    [EquipmentID] INT NOT NULL,
	[Modifier] INT NOT NULL, 
    PRIMARY KEY(StatID, EquipmentID)
)
