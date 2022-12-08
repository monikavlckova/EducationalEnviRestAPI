using EducationalEnviRestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Data;

public class UsersAPIDbContext : DbContext
{
    public UsersAPIDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }

}