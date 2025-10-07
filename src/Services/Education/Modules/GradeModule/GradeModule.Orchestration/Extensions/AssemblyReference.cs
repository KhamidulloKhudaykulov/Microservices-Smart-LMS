using System.Reflection;

namespace GradeModule.Orchestration.Extensions;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
