using Alumni.Domain.Entities;
using Alumni.Domain.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Alumni.Domain
{
    public class AppDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new FreelanceConfiguration());
            builder.ApplyConfiguration(new ContactNumberConfiguration());
            builder.ApplyConfiguration(new PostGradConfiguration());
            builder.ApplyConfiguration(new BusinessConfiguration());
            builder.ApplyConfiguration(new GovtJobConfiguration());
            builder.ApplyConfiguration(new PrivateJobConfiguration());
            builder.ApplyConfiguration(new PhotoConfiguration());
            builder.ApplyConfiguration(new NoticeConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new ActivityConfiguration());
        }
        public DbSet<Freelance> Freelances { get; set; }
        public DbSet<ContactNumber> ContactNumbers { get; set; }
        public DbSet<PostGrad> PostGrads { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<GovtJob> GovtJobs { get; set; }
        public DbSet<PrivateJob> PrivateJobs { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
