using Alumni.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumni.Domain.EntityConfigurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable(nameof(Photo));
            builder.HasKey(x => x.Id);

            builder.Property(e => e.PhotoPath)
                   .HasColumnType("varchar(256)")
                   .IsRequired();

            builder.Property(e => e.Caption)
                   .HasColumnType("varchar(256)");

            builder.HasOne(x => x.Activity)
                   .WithMany(c => c.Photos)
                   .HasForeignKey(f => f.ActivityId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Event)
                   .WithMany(c => c.Photos)
                   .HasForeignKey(f => f.EventId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Notice)
                   .WithMany(c => c.Photos)
                   .HasForeignKey(f => f.NoticeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
