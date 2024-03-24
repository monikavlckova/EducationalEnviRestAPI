using EducationalEnviRestAPI.Models;
using EducationalEnviRestAPI.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using Task = EducationalEnviRestAPI.Models.Task;

namespace EducationalEnviRestAPI.Data;

public class EduEnviAPIDbContext : DbContext
{
    public EduEnviAPIDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<ClassroomTask> ClassroomTasks { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskType> TaskTypes { get; set; }
    public DbSet<StudentTask> StudentTasks { get; set; }
    public DbSet<GroupTask> GroupTasks { get; set; }
    public DbSet<StudentGroup> StudentGroups { get; set; }
    public DbSet<Vertex> Vertices { get; set; }
    public DbSet<Edge> Edges { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClassroomConfiguration());
        modelBuilder.ApplyConfiguration(new ClassroomTaskConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new GroupTaskConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new StudentGroupConfiguration());
        modelBuilder.ApplyConfiguration(new StudentTaskConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new TaskTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
        modelBuilder.ApplyConfiguration(new EdgeConfiguration());
        modelBuilder.ApplyConfiguration(new VertexConfiguration());

        modelBuilder.Entity<Teacher>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Teacher>()
            .HasIndex(x => x.UserName)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .HasIndex(x => x.LoginCode)
            .IsUnique();
    }
}