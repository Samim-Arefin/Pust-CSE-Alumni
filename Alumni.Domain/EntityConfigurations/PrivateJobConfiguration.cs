using Alumni.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumni.Domain.EntityConfigurations
{
    public class PrivateJobConfiguration : IEntityTypeConfiguration<PrivateJob>
    {
        public void Configure(EntityTypeBuilder<PrivateJob> builder)
        {
            builder.ToTable(nameof(PrivateJob));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Designation)
                   .HasColumnType("varchar(200)")
                   .IsRequired();

            builder.Property(x => x.Organization)
                   .HasColumnType("varchar(200)")
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(p => p.PrivateJobs)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
