using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newsletter.Domain;
using Newtonsoft.Json;

namespace Newsletter.Infrastructure.Persistence.Configuration
{
    public class NewsletterTemplateConfiguration : IEntityTypeConfiguration<NewsletterTemplate>
    {
        public void Configure(EntityTypeBuilder<NewsletterTemplate> builder)
        {
            builder
                .ToTable("NewsletterTemplates");

            builder
                .HasKey(x => x.Id);

            var converter = new ValueConverter<List<string>, string>(parameters =>
                JsonConvert.SerializeObject(parameters),
                parameters => JsonConvert.DeserializeObject<List<string>>(parameters));

            builder.Property(x => x.Tokens)
                .HasConversion(converter);
        }
    }
}
