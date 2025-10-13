using CourseModule.Domain.Repositories;
using Moq;
using SharedKernel.Domain.Repositories;

namespace CourseModule.Tests.Unit.Common.Mocks;

public static class CourseRepositoryMock
{
    public static Mock<ICourseRepository> GetCourseRepsoitory()
    {
        var mock = new Mock<ICourseRepository>();

        return mock;
    }

    public static Mock<IUnitOfWork> GetUnitOfWork()
    {
        var mock = new Mock<IUnitOfWork>();
        mock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        return mock;
    }
}
