using BlazorApp1.server.DbEntity;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.server;

public class AppDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
