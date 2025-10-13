using CourseModule.Application.UseCases.Courses.Commands;
using CourseModule.Domain.Entitites;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using CourseModule.Tests.Unit.Common.Mocks;
using FluentAssertions;
using Moq;
using SharedKernel.Domain.Repositories;
using Xunit;

namespace CourseModule.Tests.Unit.Application.Commands;

public class CreateCourseCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateCourse_WhenCourseDoesNotExist()
    {
        // Arrange
        var repositoryMock = CourseRepositoryMock.GetCourseRepsoitory();
        var unitOfWorkMock = CourseRepositoryMock.GetUnitOfWork();

        var handler = new CreateCourseCommandHandler(
            repositoryMock.Object,
            unitOfWorkMock.Object);

        var command = new CreateCourseCommand(
            AccountId: Guid.NewGuid(),
            CourseName: "Backend Engineering",
            StartsAt: DateTime.UtcNow);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        repositoryMock.Verify(r => r.InsertAsync(It.IsAny<CourseEntity>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnAlreadyExists_WhenCourseExists()
    {
        // Arrange
        var repositoryMock = CourseRepositoryMock.GetCourseRepsoitory();
        var unitOfWorkMock = CourseRepositoryMock.GetUnitOfWork();

        var existingCourse = CourseEntity.Create(
            Guid.NewGuid(), Guid.NewGuid(), "Backend Engineering", DateTime.UtcNow).Value!;

        repositoryMock
            .Setup(r => r.SelectByNameAsync("Backend Engineering"))
            .ReturnsAsync(existingCourse);

        var handler = new CreateCourseCommandHandler(
            repositoryMock.Object,
            unitOfWorkMock.Object);

        var command = new CreateCourseCommand(
            AccountId: Guid.NewGuid(),
            CourseName: "Backend Engineering",
            StartsAt: DateTime.UtcNow);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();

        repositoryMock.Verify(r => r.InsertAsync(It.IsAny<CourseEntity>()), Times.Never);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}