using System.Reflection;

namespace ScheduleModule.Infrastructure.Extensions;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
