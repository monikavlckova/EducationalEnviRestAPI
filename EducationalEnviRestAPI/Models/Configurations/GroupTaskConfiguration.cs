using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalEnviRestAPI.Models.Configurations;

public class GroupTaskConfiguration : IEntityTypeConfiguration<GroupTask>
{
    public void Configure(EntityTypeBuilder<GroupTask> builder)
    {
        builder.HasOne<Group>()
            .WithMany()
            .HasForeignKey(x => x.GroupId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Taskk>()
            .WithMany()
            .HasForeignKey(x => x.TaskkId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasKey(x => new {x.GroupId, x.TaskkId});
    }
}