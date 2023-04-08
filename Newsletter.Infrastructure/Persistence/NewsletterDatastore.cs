using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Newsletter.Domain;
using Newsletter.Infrastructure.Persistence.Configuration;

namespace Newsletter.Infrastructure.Persistence;

public class NewsletterDatastore : DbContext
{
    public NewsletterDatastore(DbContextOptions<NewsletterDatastore> options) : base(options)
    {

    }

    public DbSet<Titel> Titels { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<NewsletterTemplate> NewsletterTemplates { get; set; }
    public DbSet<Domain.Newsletter> Newsletters { get; set; }

    public override int SaveChanges()
    {
        UpdateMetaData();
        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsletterTemplateConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateMetaData();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateMetaData()
    {
        var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added));

        foreach (var entity in entities)
        {
            BaseEntity domainEntity = ((BaseEntity)entity.Entity);

            if (entity.State == EntityState.Added)
            {
                domainEntity.CreatedOn = DateTime.UtcNow;
            }
        }
    }
}

internal class DataStoreDesignTimeFactory : IDesignTimeDbContextFactory<NewsletterDatastore>
{
    public NewsletterDatastore CreateDbContext(string[] args)
    {
        return new NewsletterDatastore(
            new DbContextOptionsBuilder<NewsletterDatastore>()
                .UseSqlServer(
                    "Server=.\\SQLEXPRESS;Database=newsletter;Trusted_Connection=True;MultipleActiveResultSets=true;encrypt=false;")
                .Options);
    }
}
