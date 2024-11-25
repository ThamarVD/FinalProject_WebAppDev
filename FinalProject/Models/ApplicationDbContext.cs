using Microsoft.EntityFrameworkCore;

namespace FinalProject.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
    
    public DbSet<Person> People { get; set; }
    public DbSet<Hobby> Hobbies { get; set; }
    public DbSet<Degree> Degrees { get; set; }
}