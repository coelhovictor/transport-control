using Core.Domain.Entities;
using Core.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            //builder.ApplyConfiguration(new CategoryConfiguration());
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<TransportType> TransportTypes { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<TransportTag> TransportTags { get; set; }
    }
}
