using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(x => x.TeacherId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); //TODO zmen
        
        builder.HasOne<TaskType>()
            .WithMany()
            .HasForeignKey(x => x.TaskTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); //TODO zmen

        builder.HasOne<Image>()
            .WithMany()
            .HasForeignKey(x => x.ImageId)
            .IsRequired(false);
    }
}