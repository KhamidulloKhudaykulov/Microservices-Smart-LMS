using System.Reflection;

namespace CourseModule.Orchestration.Extensions;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
