using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Heplers.Redis;
public static partial class RedisKeyHelper
{
    private static string PaymentPrefix = "payments";

    public static string GeneratePaymentKey(string accountId, int? pageNumber, int? pageSize)
    {
        if (pageNumber == null || pageSize == null)
            return $"{accountId}:{PaymentPrefix}:all";
        
        return $"{accountId}:{PaymentPrefix}:pageNumber:{pageNumber}:pageSize:{pageSize}";
    }
}
