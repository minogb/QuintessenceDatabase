CREATE PROCEDURE [dbo].[SelectItemPage]
	@page int,
	@pageSize int = 20
AS
	SELECT ItemID,Name,Description FROM Item ORDER BY ItemID OFFSET @pageSize * (@page-1) ROWS FETCH NEXT @pageSize ROWS ONLY;