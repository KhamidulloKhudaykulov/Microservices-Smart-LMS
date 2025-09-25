namespace PaymentService.Infrastructure.Extensions;

public class RedisSetting
{
    public bool Enabled { get; set; }
    public string Host { get; set; } = string.Empty;
    public int Database { get; set; }
}
