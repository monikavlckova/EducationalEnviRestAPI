using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class TaskTypeConfiguration : IEntityTypeConfiguration<TaskType>
{
    public void Configure(EntityTypeBuilder<TaskType> builder)
    {
        builder.HasOne<Image>()
            .WithMany()
            .HasForeignKey(x => x.ImageId)
            .IsRequired(false);
    }
}