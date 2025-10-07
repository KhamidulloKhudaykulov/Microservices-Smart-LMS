using LessonModule.Domain.Entities;
using LessonModule.Domain.ValueObjects.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LessonModule.Infrastructure.EfConfigurations;

public class LessonEntityConfiguration : IEntityTypeConfiguration<LessonEntity>
{
    public void Configure(EntityTypeBuilder<LessonEntity> builder)
    {
        builder.ToTable("lessons");

        builder.Property(x => x.Theme)
            .HasConversion(
                x => x.Value,
                v => Theme.Create(v).IsSuccess
                    ? Theme.Create(v).Value
                    : Theme.Create("InvalidTheme").Value);

    }
}
