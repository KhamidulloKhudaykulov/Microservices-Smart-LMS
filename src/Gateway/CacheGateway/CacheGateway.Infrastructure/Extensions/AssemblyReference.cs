using System.Reflection;

namespace CacheGateway.Infrastructure.Extensions;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
