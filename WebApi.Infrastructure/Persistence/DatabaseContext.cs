using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using WebApi.Domain.Entities.Base;

namespace WebApi.Infrastructure.Persistence;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<TagEntity> Tags { get; set; }

    public override int SaveChanges()
    {
        UpdateTimestamp();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamp();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamp()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x is { Entity: BaseEntity, State: EntityState.Modified });

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            ((BaseEntity)entity.Entity).UpdatedAt = now;
        }
    }
}