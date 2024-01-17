using EducationalEnviRestAPI.Models;
using EducationalEnviRestAPI.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Data;

public class EduEnviAPIDbContext : DbContext
{
    public EduEnviAPIDbContext(DbContextOptions options) : base(options)
    {
    }

    //public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<ClassroomTask> ClassroomsTasks { get; set; }
    public DbSet<Taskk> Tasks { get; set; }
    public DbSet<StudentTask> StudentsTasks { get; set; }
    public DbSet<GroupTask> GroupsTasks { get; set; }
    public DbSet<StudentGroup> StudentsGroups { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClassroomConfiguration());
        modelBuilder.ApplyConfiguration(new ClassroomTaskConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new GroupTaskConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new StudentGroupConfiguration());
        modelBuilder.ApplyConfiguration(new StudentTaskConfiguration());
        
        modelBuilder.Entity<Teacher>()
            .HasIndex(x => x.Email)
            .IsUnique();
        
        modelBuilder.Entity<Teacher>()
            .HasIndex(x => x.UserName)
            .IsUnique();
        
        modelBuilder.Entity<Student>()
            .HasIndex(x => x.UserName)
            .IsUnique();
    }
}