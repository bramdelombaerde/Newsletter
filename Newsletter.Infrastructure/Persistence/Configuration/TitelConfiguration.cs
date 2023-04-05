using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newsletter.Domain;

namespace Newsletter.Infrastructure.Persistence.Configuration
{
    public class TitelConfiguration : IEntityTypeConfiguration<Titel>
    {
        public void Configure(EntityTypeBuilder<Titel> builder)
        {
            builder
                .ToTable("Titels");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.ShortName)
                .IsRequired();

            builder.HasMany(x => x.Subscriptions)
                .WithOne(x => x.Titel);
        }
    }
}
