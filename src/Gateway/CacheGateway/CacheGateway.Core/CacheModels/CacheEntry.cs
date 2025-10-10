using CacheGateway.Core.CacheModels;
using CacheGateway.Core.Enums;

namespace CacheGateway.Infrastructure.CacheModels;

public class CacheEntry
{
    // masalan: "StudentService:GetById:12345")
    public string Key { get; private set; } = default!;

    // Cache'da saqlanayotgan qiymat (JSON formatda)
    public string Value { get; private set; } = default!;

    // Ma’lumot qachon cache’ga yozilgan
    public DateTime CreatedAt { get; private set; }

    // Cache amal qilish muddati (TTL — Time to Live)
    public TimeSpan Expiration { get; private set; }

    // Cache turi — Memory, Distributed, Redis va hokazo
    public CacheType Type { get; private set; }

    // Qo‘shimcha metadata ma’lumotlari (kiritilgan servis, modul va boshqalar)
    public CacheMetadata Metadata { get; private set; }

    private CacheEntry() { }

    public CacheEntry(
        string key,
        string value,
        TimeSpan expiration,
        CacheType type,
        CacheMetadata metadata)
    {
        Key = key;
        Value = value;
        Expiration = expiration;
        Type = type;
        Metadata = metadata;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cache muddati tugaganligini tekshiradi.
    /// </summary>
    public bool IsExpired() => DateTime.UtcNow - CreatedAt > Expiration;
}