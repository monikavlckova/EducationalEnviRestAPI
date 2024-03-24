using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class EdgeConfiguration : IEntityTypeConfiguration<Edge>
{
    public void Configure(EntityTypeBuilder<Edge> builder)
    {
        builder.HasOne<Vertex>()
            .WithMany()
            .HasForeignKey(x => x.fromVertexId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); //TODO zmen
        
        builder.HasOne<Vertex>()
            .WithMany()
            .HasForeignKey(x => x.toVertexId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); //TODO zmen

        builder.HasOne<Image>()
            .WithMany()
            .HasForeignKey(x => x.ImageId)
            .IsRequired(false);
    }
}