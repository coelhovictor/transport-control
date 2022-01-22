using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infra.Data.EntitiesConfiguration
{
    public class TransportTypeConfiguration : IEntityTypeConfiguration<TransportType>
    {
        public void Configure(EntityTypeBuilder<TransportType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Color).HasMaxLength(7).IsRequired();
            builder.Property(x => x.Price).HasPrecision(10, 2);

            builder.HasOne(x => x.UserProfile).WithMany(x => x.TransportTypes)
                .HasForeignKey(x => x.UserProfileId);
        }
    }
}
