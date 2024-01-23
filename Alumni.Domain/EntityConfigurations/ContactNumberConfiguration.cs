using Alumni.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumni.Domain.EntityConfigurations
{
    public class ContactNumberConfiguration : IEntityTypeConfiguration<ContactNumber>
    {
        public void Configure(EntityTypeBuilder<ContactNumber> builder)
        {
            builder.ToTable(nameof(ContactNumber));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PhoneNumber)
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(c => c.ContactNumbers)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
