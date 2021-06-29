CREATE PROCEDURE [dbo].[SelectItemTool]
	@ItemID int
AS
	SELECT * FROM Tool Where ItemID = @ItemID;