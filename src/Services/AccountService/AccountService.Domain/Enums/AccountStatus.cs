namespace AccountService.Domain.Enums;

public enum AccountStatus
{
    Inactive = 0,      // Hisob hali faollashtirilmagan
    Active = 1,        // Hisob faol
    Suspended = 2,     // Hisob vaqtincha bloklangan
    Closed = 3,        // Hisob yopilgan
    Pending = 4,       // Hisob yaratildi, lekin tasdiqlanishi kerak
    Locked = 5         // Parol xato kiritilgani uchun bloklangan
}
