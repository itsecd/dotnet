using Microsoft.EntityFrameworkCore;

namespace UserWall;

public sealed class UserWallContext : DbContext
{
    public DbSet<Post> Posts { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    public UserWallContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
}
