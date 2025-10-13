using CourseModule.Application.UseCases.Courses.Commands;
using CourseModule.Domain.Entitites;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using FluentAssertions;
using Moq;
using SharedKernel.Domain.Repositories;
using Xunit;

namespace CourseModule.Tests.Unit.Application.Commands;

public class RemoveStudentFromCourseCommandHandlerTests
{
    [Fact]
    public async Task Handle_RemoveStudentFromCourse_ShouldReturnSuccess()
    {
        // Arrange
        var repositoryMock = new Mock<ICourseRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        // Create course
        var course = CourseEntity.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Backend Course",
            DateTime.UtcNow
        ).Value!;

        // Add student to course
        var studentId = Guid.NewGuid();
        course.StudentIds.Add(studentId);

        repositoryMock
            .Setup(r => r.SelectByIdAsync(course.Id))
            .ReturnsAsync(course);

        repositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<CourseEntity>()))
            .ReturnsAsync(course);

        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new RemoveStudentFromCourseCommandHandler(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var command = new RemoveStudentFromCourseCommand(course.Id, studentId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        course.StudentIds.Should().NotContain(studentId);

        repositoryMock.Verify(r => r.UpdateAsync(course), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenStudentNotInCourse_ShouldReturnNotFoundFailure()
    {
        // Arrange
        var repositoryMock = new Mock<ICourseRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var course = CourseEntity.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Backend Course",
            DateTime.UtcNow
        ).Value!;

        repositoryMock
            .Setup(r => r.SelectByIdAsync(course.Id))
            .ReturnsAsync(course);

        var handler = new RemoveStudentFromCourseCommandHandler(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var command = new RemoveStudentFromCourseCommand(course.Id, Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_WhenCourseNotFound_ShouldReturnFailure()
    {
        // Arrange
        var repositoryMock = new Mock<ICourseRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        repositoryMock
            .Setup(r => r.SelectByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((CourseEntity?)null);

        var handler = new RemoveStudentFromCourseCommandHandler(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var command = new RemoveStudentFromCourseCommand(Guid.NewGuid(), Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}
