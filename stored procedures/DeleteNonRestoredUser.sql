
CREATE OR ALTER PROCEDURE DeleteNonRestoredUsers
AS
BEGIN
	DELETE FROM [dbo].[Users]
		WHERE [IsDelete] = 'true' AND GETDATE() >= [WhenDeleted]
END
GO
