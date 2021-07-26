CREATE PROCEDURE [dbo].[SelectEquipmentStatModifiers]
	@EquipmentID int
AS
	SELECT * FROM EquipmentStatModifier WHERE @EquipmentID = EquipmentID
