using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentService.Domain.Entities;

namespace StudentService.Persistence.Configurations;

public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("students");
        builder.HasKey(s => s.Id);

        // 🔹 FullName (owned type)
        builder.OwnsOne(s => s.FullName, fn =>
        {
            fn.Property(f => f.Value)
              .HasColumnName("full_name")
              .HasMaxLength(150)
              .IsRequired();
        });

        // 🔹 PhoneNumber
        builder.OwnsOne(s => s.PhoneNumber, pn =>
        {
            pn.Property(p => p.Value)
              .HasColumnName("phone_number")
              .HasMaxLength(20)
              .IsRequired();
        });

        // 🔹 PassportData
        builder.OwnsOne(s => s.PassportData, pd =>
        {
            pd.Property(p => p.Value)
              .HasColumnName("passport_data")
              .HasMaxLength(20)
              .IsRequired();
        });

        // 🔹 Email
        builder.OwnsOne(s => s.Email, e =>
        {
            e.Property(p => p.Value)
             .HasColumnName("email")
             .HasMaxLength(150)
             .IsRequired();
        });

        // 🔹 Status
        builder.Property(s => s.StudentStatus)
               .HasColumnName("status")
               .IsRequired();
    }
}
