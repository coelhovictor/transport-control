using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infra.Data.EntitiesConfiguration
{
    public class TransportConfiguration : IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(30);
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Price).HasPrecision(10, 2);

            builder.HasOne(x => x.UserProfile).WithMany(x => x.Transports)
                .HasForeignKey(x => x.UserProfileId);
            builder.HasOne(x => x.TransportType).WithMany(x => x.Transports)
                .HasForeignKey(x => x.TransportTypeId);
        }
    }
}
