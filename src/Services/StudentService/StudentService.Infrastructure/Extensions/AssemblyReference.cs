using System.Reflection;

namespace StudentService.Infrastructure.Extensions;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
