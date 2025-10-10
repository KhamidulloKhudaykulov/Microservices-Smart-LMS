using System.Reflection;

namespace ScheduleModule.Orchestration.Extensions;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
