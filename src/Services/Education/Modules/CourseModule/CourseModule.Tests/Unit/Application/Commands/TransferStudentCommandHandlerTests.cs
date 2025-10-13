using CourseModule.Application.UseCases.Courses.Commands;
using CourseModule.Domain.Entitites;
using CourseModule.Tests.Unit.Common.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourseModule.Tests.Unit.Application.Commands;

public class TransferStudentCommandHandlerTests
{
    [Fact]
    public async Task Handle_Shoudld_TransferStudent_WhithValidData()
    {
        // Arrange
        var repositoryMock = CourseRepositoryMock.GetCourseRepsoitory();
        var unitOfWorkMock = CourseRepositoryMock.GetUnitOfWork();

        var studentId = Guid.NewGuid();
        var fromCourse = CourseEntity.Create(Guid.NewGuid(), Guid.NewGuid(), "From Course", DateTime.UtcNow).Value!;
        var toCourse = CourseEntity.Create(Guid.NewGuid(), Guid.NewGuid(), "To Course", DateTime.UtcNow).Value!;

        fromCourse.AddStudent(studentId);

        repositoryMock
             .Setup(r => r.SelectByIdAsync(fromCourse.Id))
             .ReturnsAsync(fromCourse);

        repositoryMock
            .Setup(r => r.SelectByIdAsync(toCourse.Id))
            .ReturnsAsync(toCourse);

        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new TransferStudentCommandHandler(
            repositoryMock.Object, 
            unitOfWorkMock.Object);

        var command = new TransferStudentCommand(fromCourse.Id, toCourse.Id, studentId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();


        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
