using CourseModule.Domain.Entitites;
using CourseModule.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace CourseModule.Tests.Unit.Domain;

public class CourseEntityTests
{
    [Fact]
    public void Create_ShouldReturn_SuccessResult_WithValidData()
    {
        // Arrange
        var id = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var name = "Backend Engineering";
        var startsAt = DateTime.UtcNow;

        // Act
        var result = CourseEntity.Create(id, accountId, name, startsAt);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be(name);
        result.Value.StartsAt.Should().Be(startsAt);
        result.Value.Status.Should().Be(CourseStatus.Opened);
    }

    [Fact]
    public void AddStudent_Should_AddStudentId_ToList()
    {
        // Arrange
        var course = CourseEntity.Create(Guid.NewGuid(), Guid.NewGuid(), "Backend", DateTime.UtcNow).Value;
        var studentId = Guid.NewGuid();

        // Act
        var result = course.AddStudent(studentId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        course.StudentIds.Should().Contain(studentId);
    }

    [Fact]
    public void AddTeacher_Should_AddTeacherId_ToList()
    {
        // Arrange
        var course = CourseEntity.Create(Guid.NewGuid(), Guid.NewGuid(), "Physics", DateTime.UtcNow).Value!;
        var teacherId = Guid.NewGuid();

        // Act
        var result = course.AddTeacher(teacherId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        course.TeacherIds.Should().Contain(teacherId);
    }

    [Fact]
    public void UpdateCourseName_Should_ChangeName()
    {
        // Arrange
        var course = CourseEntity.Create(Guid.NewGuid(), Guid.NewGuid(), "Old Name", DateTime.UtcNow).Value!;

        // Act
        course.UpdateCourseName("New Name");

        // Assert
        course.Name.Should().Be("New Name");
    }

    [Fact]
    public void UpdateStartDate_Should_ChangeStartDate()
    {
        // Arrange
        var course = CourseEntity.Create(Guid.NewGuid(), Guid.NewGuid(), "Course", DateTime.UtcNow).Value!;
        var newDate = DateTime.UtcNow.AddDays(3);

        // Act
        course.UpdateStartDate(newDate);

        // Assert
        course.StartsAt.Should().Be(newDate);
    }

    [Fact]
    public void ChangeStatus_Should_UpdateStatus()
    {
        // Arrange
        var course = CourseEntity.Create(Guid.NewGuid(), Guid.NewGuid(), "Course", DateTime.UtcNow).Value!;

        // Act
        course.ChangeStatus(CourseStatus.Closed);

        // Assert
        course.Status.Should().Be(CourseStatus.Closed);
    }

    [Fact]
    public void Open_Close_Block_Should_Use_StatePattern()
    {
        // Arrange
        var course = CourseEntity.Create(Guid.NewGuid(), Guid.NewGuid(), "Pattern Test", DateTime.UtcNow).Value!;

        // Act
        var openResult = course.Open();
        var closeResult = course.Close();
        var blockResult = course.Block();

        // Assert
        openResult.IsSuccess.Should().BeTrue();
        closeResult.IsSuccess.Should().BeTrue();
        blockResult.IsSuccess.Should().BeFalse();
    }
}
