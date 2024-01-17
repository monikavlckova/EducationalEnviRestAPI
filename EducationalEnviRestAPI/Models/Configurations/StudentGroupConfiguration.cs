using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
{
    public void Configure(EntityTypeBuilder<StudentGroup> builder)
    {
        builder.HasOne<Group>()
            .WithMany()
            .HasForeignKey(x => x.GroupId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasKey(x => new { x.StudentId, x.GroupId});
    }
}