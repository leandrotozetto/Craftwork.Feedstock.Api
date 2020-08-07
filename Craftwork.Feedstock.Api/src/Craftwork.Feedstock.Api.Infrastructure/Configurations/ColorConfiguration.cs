using Craftwork.Feedstock.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Craftwork.Feedstock.Api.Infrastructure.Configurations
{
    internal class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public static ColorConfiguration New()
        {
            return new ColorConfiguration();
        }

        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(x => x.ColorId.Value);

            builder.Property(x => x.ColorId.Value)
                .HasField("ColorId")
                .IsRequired();

            builder.Property(x => x.TenantId.Value)
                .HasField("TenantId")
                .IsRequired();

            builder.Property(x => x.CreationDate)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Status.Value)
                .HasField("Status")
                .IsRequired();

            builder.Property(x => x.UpdateDate);
        }
    }
}
