CREATE PROCEDURE [dbo].[SelectSubSlotEnums]
	@ParentSlot int,
	@Flags int = null
AS
	SELECT * FROM [SlotEnum] WHERE ParentSlot = @ParentSlot AND (Slot & @Flags) > 0 ORDER BY Slot ASC