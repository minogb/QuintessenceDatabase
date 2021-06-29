CREATE PROCEDURE [dbo].[SelectAllSlotEnums]
	@ParentSlot int = NULL
AS
	SELECT * FROM [SlotEnum] WHERE (ParentSlot = @ParentSlot OR (ISNULL(ParentSlot,@ParentSlot) IS NULL)) ORDER BY Slot ASC