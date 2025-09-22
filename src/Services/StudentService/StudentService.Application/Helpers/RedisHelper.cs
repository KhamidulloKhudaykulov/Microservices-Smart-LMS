namespace StudentService.Application.Helpers;

public static class RedisHelper
{
    private static string GenerateKey(string prefix, IDictionary<string, string> args)
    {
        if (args == null || !args.Any())
            return prefix.ToLower();

        var keyParts = args.Select(kvp => $"{kvp.Key.ToLower()}:{kvp.Value.ToLower()}");
        return $"{prefix.ToLower()}:{string.Join(":", keyParts)}";
    }

    public static string GenerateUserKey(int pageNumber, int pageSize)
    {
        var prefix = GenerateUserPrefix();

        var args = new Dictionary<string, string>
            {
                { "pagenumber", pageNumber.ToString() },
                { "pagesize", pageSize.ToString() }
            };

        return GenerateKey(prefix, args);
    }

    private static string GenerateUserPrefix()
        => $"students";
}