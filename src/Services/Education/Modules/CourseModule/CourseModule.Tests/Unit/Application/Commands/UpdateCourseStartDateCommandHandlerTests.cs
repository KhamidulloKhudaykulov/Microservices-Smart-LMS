using CourseModule.Application.UseCases.Courses.Commands;
using CourseModule.Domain.Entitites;
using CourseModule.Domain.Repositories;
using FluentAssertions;
using Moq;
using SharedKernel.Domain.Repositories;
using Xunit;

namespace CourseModule.Tests.Unit.Application.Commands;

public class UpdateCourseStartDateCommandHandlerTests
{
    [Fact]
    public async Task Handle_UpdateCourseStartDate_ShouldReturnSuccess()
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

        var newStartDate = DateTime.UtcNow.AddDays(10);

        repositoryMock
            .Setup(r => r.SelectByIdAsync(course.Id))
            .ReturnsAsync(course);

        repositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<CourseEntity>()))
            .ReturnsAsync((CourseEntity?)null);

        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new UpdateCourseStartDateCommandHandler(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var command = new UpdateCourseStartDateCommand(course.Id, newStartDate);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        course.StartsAt.Should().Be(newStartDate);

        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<CourseEntity>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
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

        var handler = new UpdateCourseStartDateCommandHandler(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var command = new UpdateCourseStartDateCommand(Guid.NewGuid(), DateTime.UtcNow);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
    }
}