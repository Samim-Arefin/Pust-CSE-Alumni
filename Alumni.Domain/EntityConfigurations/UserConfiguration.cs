using Alumni.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumni.Domain.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.FullName)
                   .HasColumnType("varchar(50)");

            builder.Property(e => e.NickName)
                   .HasColumnType("varchar(20)");

            builder.Property(e => e.Gender)
                   .HasColumnType("int");

            builder.Property(e => e.Session)
                   .HasColumnType("varchar(20)");

            builder.Property(e => e.Roll)
                  .HasColumnType("varchar(20)");

            builder.Property(e => e.Batch)
                   .HasColumnType("int");

            builder.Property(e => e.Status)
                   .HasColumnType("int");

            builder.Property(e => e.PhotoPath)
                   .HasColumnType("varchar(100)");

            builder.Property(e => e.GithubUrl)
                   .HasColumnType("varchar(100)");

            builder.Property(e => e.LinkedInUrl)
                   .HasColumnType("varchar(100)");

            builder.Property(e => e.FacebookUrl)
                   .HasColumnType("varchar(100)");

            builder.Property(e => e.CurrentAddress)
                   .HasColumnType("varchar(200)");

            builder.Property(e => e.PermanantAddress)
                   .HasColumnType("varchar(200)");
        }
    }
}
