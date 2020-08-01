using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codenation.Challenge.Models
{
    public class AccelerationMap : IEntityTypeConfiguration<Acceleration>
    {
        public void Configure(EntityTypeBuilder<Acceleration> builder)
        {
            builder.ToTable("acceleration");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                 .HasColumnName("id")
                 .IsRequired();

            builder.Property(a => a.Name)
                 .HasColumnName("name")
                 .HasMaxLength(100)
                 .IsRequired();

            builder.Property(a => a.Slug)
                 .HasColumnName("slug")
                 .HasMaxLength(50)
                 .IsRequired();

            builder.Property(a => a.ChallengeId)
                 .HasColumnName("challenge_id")
                 .IsRequired();

            builder.Property(a => a.CreatedAt)
                 .HasColumnName("created_at")
                 .IsRequired();

            builder.HasOne(a => a.Challenge)
                .WithMany(a => a.Accelerations)
                .HasForeignKey(a => a.ChallengeId);
        }

    }
}
