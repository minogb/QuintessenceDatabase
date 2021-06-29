CREATE PROCEDURE [dbo].[SelectItem]
	@ItemID int = 0
AS
	SELECT Name,Description,StackSize FROM Item WHERE ItemID = @ItemID;
	 