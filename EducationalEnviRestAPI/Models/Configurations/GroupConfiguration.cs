using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasOne<Classroom>()
            .WithMany()
            .HasForeignKey(x => x.ClassroomId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); //TODO zmen
    }
}