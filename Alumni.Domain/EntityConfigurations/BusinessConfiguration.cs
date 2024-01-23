using Alumni.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumni.Domain.EntityConfigurations
{
    public class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.ToTable(nameof(Business));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BusinessType)
                   .HasColumnType("int");

            builder.HasOne(x => x.User)
                   .WithMany(b => b.Businesses)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
