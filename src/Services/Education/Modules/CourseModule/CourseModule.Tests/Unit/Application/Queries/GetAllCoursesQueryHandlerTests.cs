using CourseModule.Application.UseCases.Courses.Queries;
using CourseModule.Domain.Entitites;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourseModule.Tests.Unit.Application.Queries;

public class GetAllCoursesQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenCoursesExist_ShouldReturnSuccessWithData()
    {
        // Arrange
        var repositoryMock = new Mock<ICourseRepository>();

        var accountId = Guid.NewGuid();

        var courses = new List<CourseEntity>
        {
            CourseEntity.Create(Guid.NewGuid(), accountId, "Backend", DateTime.UtcNow).Value!,
            CourseEntity.Create(Guid.NewGuid(), accountId, "Frontend", DateTime.UtcNow.AddDays(1)).Value!
        };

        repositoryMock
            .Setup(r => r.SelectAllByAccountIdAsync(accountId))
            .ReturnsAsync(courses);

        var handler = new GetAllCoursesQueryHandler(repositoryMock.Object);
        var query = new GetAllCoursesQuery(accountId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().HaveCount(2);

        var courseList = result.Value.ToList();
        courseList[0].Name.Should().Be("Backend");
        courseList[1].Name.Should().Be("Frontend");

        repositoryMock.Verify(r => r.SelectAllByAccountIdAsync(accountId), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenNoCourses_ShouldReturnNotFoundFailure()
    {
        // Arrange
        var repositoryMock = new Mock<ICourseRepository>();
        var accountId = Guid.NewGuid();

        repositoryMock
            .Setup(r => r.SelectAllByAccountIdAsync(accountId))
            .ReturnsAsync(new List<CourseEntity>()); // Bo‘sh ro‘yxat

        var handler = new GetAllCoursesQueryHandler(repositoryMock.Object);
        var query = new GetAllCoursesQuery(accountId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_WhenRepositoryReturnsNull_ShouldReturnNotFoundFailure()
    {
        // Arrange
        var repositoryMock = new Mock<ICourseRepository>();
        var accountId = Guid.NewGuid();

        repositoryMock
            .Setup(r => r.SelectAllByAccountIdAsync(accountId))
            .ReturnsAsync((IEnumerable<CourseEntity>?)null);

        var handler = new GetAllCoursesQueryHandler(repositoryMock.Object);
        var query = new GetAllCoursesQuery(accountId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}