using Alumni.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumni.Domain.EntityConfigurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable(nameof(Activity));
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Title)
                   .HasColumnType("varchar(256)")
                   .IsRequired();

            builder.Property(e => e.Description)
                   .HasColumnType("varchar(1024)");
        }
    }
}
