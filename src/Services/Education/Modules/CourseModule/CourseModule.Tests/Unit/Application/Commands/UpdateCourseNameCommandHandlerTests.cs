using CourseModule.Application.UseCases.Courses.Commands;
using CourseModule.Domain.Entitites;
using CourseModule.Tests.Unit.Common.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourseModule.Tests.Unit.Application.Commands;

public class UpdateCourseNameCommandHandlerTests
{
    [Fact]
    public async Task Handle_UpdateCourseName_ShouldReturnsSuccess()
    {
        // Arrange
        var repositoryMock = CourseRepositoryMock.GetCourseRepsoitory();
        var unitOfWorkMock = CourseRepositoryMock.GetUnitOfWork();

        var course = CourseEntity.Create(Guid.NewGuid(), Guid.NewGuid(), "Backend", DateTime.UtcNow).Value!;

        repositoryMock
            .Setup(r => r.SelectByIdAsync(course.Id))
            .ReturnsAsync(course);

        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(CancellationToken.None))
            .ReturnsAsync(1);

        var handler = new UpdateCourseNameCommandHandler(
            repositoryMock.Object,
            unitOfWorkMock.Object);

        var command = new UpdateCourseNameCommand(course.Id, "FrontEnd");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
