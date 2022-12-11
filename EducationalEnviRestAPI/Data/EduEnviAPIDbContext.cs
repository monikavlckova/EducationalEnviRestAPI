using EducationalEnviRestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Data;

public class EduEnviAPIDbContext : DbContext
{
    public EduEnviAPIDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<User> Students { get; set; }
    public DbSet<User> Teachers { get; set; }
    public DbSet<User> Groups { get; set; }
    public DbSet<User> Classrooms { get; set; }
    public DbSet<User> Tasks { get; set; }
    public DbSet<User> StudentsTasks { get; set; }
    public DbSet<User> GroupsTasks { get; set; }
    public DbSet<User> StudentsGroups { get; set; }

}