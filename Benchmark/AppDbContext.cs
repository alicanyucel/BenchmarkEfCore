
using Microsoft.EntityFrameworkCore;

namespace Benchmark;


public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Data Source=DESKTOP-L6NJT48\\SQLEXPRESS;Initial Catalog=Benchmark;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Alican", Age = 30 },
            new User { Id = 2, Name = "Eser", Age = 25 }
        );
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
