using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasOne<Classroom>()
            .WithMany()
            .HasForeignKey(x => x.ClassroomId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); //TODO zmen
        
        builder.HasOne<Image>()
            .WithMany()
            .HasForeignKey(x => x.ImageId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict); //TODO zmen
    }
}