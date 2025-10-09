IF OBJECT_ID('dbo.BulkInsertHomework', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkInsertHomework;
GO

IF TYPE_ID('dbo.HomeworkTableType') IS NOT NULL
    DROP TYPE dbo.HomeworkTableType;
GO
