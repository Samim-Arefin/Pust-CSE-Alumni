using Alumni.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Domain.EntityConfigurations
{
    public class PostGradConfiguration : IEntityTypeConfiguration<PostGrad>
    {
        public void Configure(EntityTypeBuilder<PostGrad> builder)
        {
            builder.ToTable("PostGraduation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PostGradDegree)
                   .HasColumnType("varchar(200)")
                   .IsRequired();

            builder.Property(x => x.PostGradField)
                   .HasColumnType("varchar(200)")
                   .IsRequired();

            builder.Property(x => x.PostGradUniversity)
                   .HasColumnType("varchar(200)")
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(p => p.PostGrads)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
