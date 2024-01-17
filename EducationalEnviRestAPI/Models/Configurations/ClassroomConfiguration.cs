using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(x => x.TeacherId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); //TODO zmen
    }
}