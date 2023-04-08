using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Newsletter.Infrastructure.Persistence.Configuration
{
    public class NewsletterConfiguration : IEntityTypeConfiguration<Domain.Newsletter>
    {
        public void Configure(EntityTypeBuilder<Domain.Newsletter> builder)
        {
            builder
                .ToTable("Newsletters");

            builder
                .HasKey(x => x.Id);
        }
    }
}
