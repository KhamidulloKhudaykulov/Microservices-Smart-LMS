using CourseModule.Application.UseCases.Courses.Queries;
using CourseModule.Domain.Entitites;
using CourseModule.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourseModule.Tests.Unit.Application.Queries;

public class GetCourseByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenCourseExists_ShouldReturnSuccessWithData()
    {
        // Arrange
        var repositoryMock = new Mock<ICourseRepository>();

        var course = CourseEntity.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Backend Development",
            DateTime.UtcNow
        ).Value!;

        repositoryMock
            .Setup(r => r.SelectByIdAsync(course.Id))
            .ReturnsAsync(course);

        var handler = new GetCourseByIdQueryHandler(repositoryMock.Object);
        var query = new GetCourseByIdQuery(course.Id);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(course.Id);
        result.Value.Name.Should().Be("Backend Development");
        result.Value.StartsAt.Should().Be(course.StartsAt);

        repositoryMock.Verify(r => r.SelectByIdAsync(course.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenCourseNotFound_ShouldReturnFailure()
    {
        // Arrange
        var repositoryMock = new Mock<ICourseRepository>();
        var courseId = Guid.NewGuid();

        repositoryMock
            .Setup(r => r.SelectByIdAsync(courseId))
            .ReturnsAsync((CourseEntity?)null);

        var handler = new GetCourseByIdQueryHandler(repositoryMock.Object);
        var query = new GetCourseByIdQuery(courseId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}