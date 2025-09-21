using System.Globalization;
using System.Text;

namespace StudentService.Application.Helpers;

public static class RedisHelper
{
    public static string GenerateKey(string prefix, object filters)
    {
        if (filters == null) return prefix;

        var sb = new StringBuilder(prefix);

        var properties = filters.GetType().GetProperties();
        foreach (var p in properties)
        {
            var value = p.GetValue(filters);
            if (value == null) continue;

            sb.Append(":");

            if (value is System.Collections.IEnumerable enumerable && !(value is string))
            {
                var items = enumerable.Cast<object>()
                    .Select(x => x?.ToString())
                    .Where(x => !string.IsNullOrEmpty(x))
                    .OrderBy(x => x);

                sb.Append(string.Join("_", items));
            }
            else if (value is bool b)
            {
                sb.Append(b ? "1" : "0");
            }
            else if (value is IFormattable formattable)
            {
                sb.Append(formattable.ToString(null, CultureInfo.InvariantCulture));
            }
            else
            {
                sb.Append(value.ToString());
            }
        }

        return sb.ToString();
    }
}