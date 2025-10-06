using System.Reflection;

namespace CourseModel.Orchestration.Application.Extensions;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
