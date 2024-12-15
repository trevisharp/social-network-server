using Microsoft.EntityFrameworkCore;

namespace Model;

public class Context : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasOne(u => u.Avatar)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Entity<User>()
            .HasOne(u => u.Banner)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithMany(u => u.Following);
        
        builder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.Author)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Post>()
            .HasMany(p => p.Posts)
            .WithOne(p => p.Parent)
            .OnDelete(DeleteBehavior.SetNull);
    }
}