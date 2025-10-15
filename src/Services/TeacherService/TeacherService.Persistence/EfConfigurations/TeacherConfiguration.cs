using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherService.Domain.Aggregates;
using TeacherService.Domain.ValueObjects.Teachers;

namespace TeacherService.Persistence.EfConfigurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("teachers");

        builder.Property(x => x.Name)
            .HasConversion(x => x!.Value, v => TeacherName.Create(v).Value);

        builder.Property(x => x.Email)
            .HasConversion(x => x!.Value, v => Email.Create(v).Value);

        builder.Property(x => x.Surname)
            .HasConversion(x => x!.Value, v => TeacherSurname.Create(v).Value);

        builder.Property(x => x.PhoneNumber)
            .HasConversion(x => x!.Value, v => PhoneNumber.Create(v).Value);
    }
}
