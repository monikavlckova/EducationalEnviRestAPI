using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class VertexConfiguration : IEntityTypeConfiguration<Vertex>
{
    public void Configure(EntityTypeBuilder<Vertex> builder)
    {
        builder.HasOne<Task>()
            .WithMany()
            .HasForeignKey(x => x.TaskId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); //TODO zmen
        
        builder.HasOne<Image>()
            .WithMany()
            .HasForeignKey(x => x.ImageId)
            .IsRequired(false);
    }
}