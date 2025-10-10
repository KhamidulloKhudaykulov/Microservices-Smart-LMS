namespace CacheGateway.Core.CacheModels;

/// <summary>
/// Cache haqida kontekstual metadata ma’lumotlari
/// </summary>
public class CacheMetadata
{
    /// <summary>
    /// Cache’ni yaratgan modul yoki servis nomi (masalan: "StudentService" yoki "ScheduleModule")
    /// </summary>
    public string Source { get; private set; } = default!;

    /// <summary>
    /// Cache’da saqlanayotgan ma’lumot turi (masalan: "StudentProfile", "CourseInfo" va hokazo)
    /// </summary>
    public string DataType { get; private set; } = default!;

    /// <summary>
    /// Cache yaratgan foydalanuvchi yoki tizim (opsional)
    /// </summary>
    public string? CreatedBy { get; private set; }

    /// <summary>
    /// Cache so‘nggi marta yangilangan vaqt
    /// </summary>
    public DateTime LastUpdatedAt { get; private set; }

    private CacheMetadata() { }

    public CacheMetadata(string source, string dataType, string? createdBy = null)
    {
        Source = source;
        DataType = dataType;
        CreatedBy = createdBy;
        LastUpdatedAt = DateTime.UtcNow;
    }

    public void UpdateTimestamp()
    {
        LastUpdatedAt = DateTime.UtcNow;
    }
}
