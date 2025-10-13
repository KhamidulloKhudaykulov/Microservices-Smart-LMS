namespace Integration.Infrastucture.Configurations;

public class RedisSettings
{
    public string? Host { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool IsEnabled { get; set; } = true;
}
