using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Newsletter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsletter.Infrastructure.Persistence;

public class NewsletterDatastore : DbContext
{
    public NewsletterDatastore(DbContextOptions<NewsletterDatastore> options) : base(options)
    {
        
    }

    public DbSet<Titel> Titels { get; set; }
}

internal class DataStoreDesignTimeFactory : IDesignTimeDbContextFactory<NewsletterDatastore>
{
    public NewsletterDatastore CreateDbContext(string[] args)
    {
        return new NewsletterDatastore(
            new DbContextOptionsBuilder<NewsletterDatastore>()
                .UseSqlServer(
                    "Server=localhost;Initial Catalog=sqldbnewsletter;Integrated Security=true;MultipleActiveResultSets=true")
                .Options);
    }
}
