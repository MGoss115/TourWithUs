#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace TourWith.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<Destination> Destinations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Schedule> Scheduled { get; set; }
}