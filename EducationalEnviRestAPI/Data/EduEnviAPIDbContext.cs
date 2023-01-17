using EducationalEnviRestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Data;

public class EduEnviAPIDbContext : DbContext
{
    public EduEnviAPIDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Taskk> Tasks { get; set; }
    public DbSet<StudentTask> StudentsTasks { get; set; }
    public DbSet<GroupTask> GroupsTasks { get; set; }
    public DbSet<StudentGroup> StudentsGroups { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasOne(typeof(User))
            .WithMany()
            .HasForeignKey("Id");
        
        modelBuilder.Entity<Teacher>()
            .HasOne(typeof(User))
            .WithMany()
            .HasForeignKey("Id");
        
       /* modelBuilder.Entity<Classroom>()
            .HasOne(typeof(Teacher))
            .WithMany()
            .HasForeignKey("TeacherId");
        
        modelBuilder.Entity<Group>()
            .HasOne(typeof(Teacher))
            .WithMany()
            .HasForeignKey("TeacherId");*/
        
        modelBuilder.Entity<GroupTask>()
            .HasOne(typeof(Group))
            .WithMany()
            .HasForeignKey("GroupId");
        
        modelBuilder.Entity<GroupTask>()
            .HasOne(typeof(Taskk))
            .WithMany()
            .HasForeignKey("TaskId");
        
        modelBuilder.Entity<Student>()
            .HasOne(typeof(Classroom))
            .WithMany()
            .HasForeignKey("ClassroomId");
        
       /* modelBuilder.Entity<StudentGroup>()
            .HasOne(typeof(Student))
            .WithMany()
            .HasForeignKey("StudentId");
        
        modelBuilder.Entity<StudentGroup>()
            .HasOne(typeof(Group))
            .WithMany()
            .HasForeignKey("GroupId");
        
        modelBuilder.Entity<StudentTask>()
            .HasOne(typeof(Student))
            .WithMany()
            .HasForeignKey("StudentId");
        
        modelBuilder.Entity<StudentTask>()
            .HasOne(typeof(Taskk))
            .WithMany()
            .HasForeignKey("TaskId");*/
    }
}