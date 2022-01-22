using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infra.Data.EntitiesConfiguration
{
    public class TransportTagConfiguration : IEntityTypeConfiguration<TransportTag>
    {
        public void Configure(EntityTypeBuilder<TransportTag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Transport).WithMany(x => x.TransportTags)
                .HasForeignKey(x => x.TransportId);
            builder.HasOne(x => x.Tag).WithMany(x => x.TransportTags)
                .HasForeignKey(x => x.TagId);
        }
    }
}
