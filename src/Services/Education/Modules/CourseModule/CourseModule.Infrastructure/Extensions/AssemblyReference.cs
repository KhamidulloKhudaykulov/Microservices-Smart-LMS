using System.Reflection;

namespace CourseModule.Infrastructure.Extensions;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
