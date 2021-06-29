CREATE PROCEDURE [dbo].[SelectEquipmentItem]
	@ItemID int
AS
	SELECT Item.Name,Item.Description,Item.StackSize,Equipment.Slot, slot1.Name AS [SlotName],Equipment.ParentSlot,slot2.Name AS [ParentSlotName],Equipment.AvailableSlots
	FROM Item LEFT JOIN Equipment ON (Item.ItemID = Equipment.ItemID) 
	LEFT JOIN SlotEnum slot1 ON (Equipment.Slot = slot1.Slot 
	AND (Equipment.ParentSlot = slot1.ParentSlot OR (ISNULL(Equipment.ParentSlot,slot1.ParentSlot) IS NULL))) 
	LEFT JOIN SlotEnum slot2 ON (Equipment.ParentSlot = slot2.Slot AND (slot2.ParentSlot IS NULL))  WHERE Item.ItemID = @ItemID;