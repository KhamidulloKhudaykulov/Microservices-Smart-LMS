CREATE TYPE dbo.HomeworkTableType AS TABLE
(
    Id UNIQUEIDENTIFIER,
    Title NVARCHAR(250),
    Description NVARCHAR(MAX),
    MaxScore DECIMAL(18,2),
    CreatedDate DATETIME2,
    EndTime DATETIME2,
    Status INT,
    LessonId UNIQUEIDENTIFIER,
    CourseId UNIQUEIDENTIFIER,
    CreatedBy UNIQUEIDENTIFIER
);
GO

CREATE PROCEDURE dbo.Add_BulkInsertHomework
    @Homeworks dbo.HomeworkTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Homeworks (Id, Title, Description, MaxScore, CreatedDate, EndTime, Status, LessonId, CourseId, CreatedBy)
    SELECT Id, Title, Description, MaxScore, CreatedDate, EndTime, Status, LessonId, CourseId, CreatedBy
    FROM @Homeworks;
END
GO