using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class StudentTaskConfiguration : IEntityTypeConfiguration<StudentTask>
{
    public void Configure(EntityTypeBuilder<StudentTask> builder)
    {
        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Task>()
            .WithMany()
            .HasForeignKey(x => x.TaskId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasKey(x => new {x.StudentId, TaskkId = x.TaskId});
    }
}