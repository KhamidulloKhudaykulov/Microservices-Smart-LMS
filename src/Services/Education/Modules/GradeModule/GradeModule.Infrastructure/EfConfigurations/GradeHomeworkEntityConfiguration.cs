using GradeModule.Domain.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GradeModule.Infrastructure.EfConfigurations;

public class GradeHomeworkEntityConfiguration : IEntityTypeConfiguration<GradeHomeworkEntity>
{
    public void Configure(EntityTypeBuilder<GradeHomeworkEntity> builder)
    {
        builder.ToTable("homework_grades");
    }
}
