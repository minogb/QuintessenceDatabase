CREATE PROCEDURE [dbo].[DeleteItem]
	@ItemID int
AS
BEGIN
	DECLARE @ItemRefs int;
	DECLARE @player int;
	DECLARE player_cursor CURSOR FOR SELECT PlayerCharacterID FROM PlayerCharacter;
	SET @ItemRefs = (SELECT Stack.StackID FROM Stack WHERE ItemID = @ItemID);
	DELETE FROM SkillRequirement WHERE ItemID = @ItemID;
	DELETE FROM Tool WHERE ItemID = @ItemID;
	DELETE FROM Equipment WHERE ItemID = @ItemID;
	DELETE FROM Inventory WHERE StackID IN (@ItemRefs);
	OPEN player_cursor
	FETCH NEXT FROM player_cursor INTO @player;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		--TODO: REDO player equipment slots
		FETCH NEXT FROM player_cursor INTO @player;
	END
	DELETE FROM Stack Where ItemID = @ItemID;
	DELETE FROM Item WHERE ItemID = @ItemID;
END