using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newsletter.Domain;

namespace Newsletter.Infrastructure.Persistence.Configuration
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder
                .ToTable("Subscriptions");

            builder
                .HasKey(x => x.Id);
        }
    }
}
