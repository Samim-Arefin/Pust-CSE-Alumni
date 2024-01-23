using Alumni.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumni.Domain.EntityConfigurations
{
    public class FreelanceConfiguration : IEntityTypeConfiguration<Freelance>
    {
        public void Configure(EntityTypeBuilder<Freelance> builder)
        {
            builder.ToTable(nameof(Freelance));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.WorkingFields)
                   .HasColumnType("varchar(200)")
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithOne(fr => fr.Freelance)
                   .HasForeignKey<Freelance>(f => f.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
