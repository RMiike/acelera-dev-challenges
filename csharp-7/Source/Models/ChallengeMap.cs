using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codenation.Challenge.Models
{
    public class ChallengeMap : IEntityTypeConfiguration<Challenge>
    {
        public void Configure(EntityTypeBuilder<Challenge> builder)
        {

            builder.ToTable("challenge");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Slug)
               .HasColumnName("slug")
               .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();
        }
    }
}
