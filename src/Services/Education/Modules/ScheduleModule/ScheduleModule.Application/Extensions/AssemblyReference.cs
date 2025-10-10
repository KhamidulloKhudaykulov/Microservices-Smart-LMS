using System.Reflection;

namespace ScheduleModule.Application.Extensions;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
