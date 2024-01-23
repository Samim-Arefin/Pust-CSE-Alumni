using Alumni.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumni.Domain.EntityConfigurations
{
    public class GovtJobConfiguration : IEntityTypeConfiguration<GovtJob>
    {
        public void Configure(EntityTypeBuilder<GovtJob> builder)
        {
            builder.ToTable(nameof(GovtJob));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Designation)
                   .HasColumnType("varchar(200)")
                   .IsRequired();

            builder.Property(x => x.Organization)
                   .HasColumnType("varchar(200)")
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(g => g.GovtJobs)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
