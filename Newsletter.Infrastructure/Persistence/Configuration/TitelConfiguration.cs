using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newsletter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
