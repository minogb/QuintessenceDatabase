CREATE TABLE [dbo].[SkillRequirement]
(
	[SkillID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ItemID] INT NOT NULL, 
    [Skill] INT NOT NULL,
    [Level] INT NOT NULL,
    [IsModifier] BIT NOT NULL DEFAULT 0, 
    FOREIGN KEY ([ItemID]) REFERENCES [Item]([ItemID])
)
