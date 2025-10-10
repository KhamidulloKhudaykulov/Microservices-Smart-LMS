using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleModule.Domain.Entities;

namespace ScheduleModule.Infrastructure.EfConfigurations;

public class LessonScheduleConfiguration : IEntityTypeConfiguration<LessonScheduleEntity>
{
    public void Configure(EntityTypeBuilder<LessonScheduleEntity> builder)
    {
        builder.ToTable("schedule_lesson");
    }
}
