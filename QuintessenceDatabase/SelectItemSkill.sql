CREATE PROCEDURE [dbo].[SelectItemSkill]
	@ItemID int
AS
	SELECT * FROM SkillRequirement Where ItemID = @ItemID;