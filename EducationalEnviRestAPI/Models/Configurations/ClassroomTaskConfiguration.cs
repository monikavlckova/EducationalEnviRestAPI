using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class ClassroomTaskConfiguration : IEntityTypeConfiguration<ClassroomTask>
{
    public void Configure(EntityTypeBuilder<ClassroomTask> builder)
    {
        builder.HasOne<Classroom>()
            .WithMany()
            .HasForeignKey(x => x.ClassroomId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Task>()
            .WithMany()
            .HasForeignKey(x => x.TaskId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasKey(x => new {x.ClassroomId, TaskkId = x.TaskId});
    }
}