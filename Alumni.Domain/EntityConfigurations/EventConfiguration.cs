using Alumni.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumni.Domain.EntityConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable(nameof(Event));
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Title)
                   .HasColumnType("varchar(256)")
                   .IsRequired();

            builder.Property(e => e.Description)
                   .HasColumnType("varchar(1024)");
        }
    }
}
