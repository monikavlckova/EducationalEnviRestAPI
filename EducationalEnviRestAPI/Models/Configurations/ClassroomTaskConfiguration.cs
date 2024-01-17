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
        
        builder.HasOne<Taskk>()
            .WithMany()
            .HasForeignKey(x => x.TaskkId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasKey(x => new {x.ClassroomId, x.TaskkId});
    }
}