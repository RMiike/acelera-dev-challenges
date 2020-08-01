using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codenation.Challenge.Models
{
    public class CandidateMap : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("candidate");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(c => c.AccelerationId)
              .HasColumnName("acceleration_id")
              .IsRequired();

            builder.Property(c => c.CompaniyId)
              .HasColumnName("company_id")
              .IsRequired();

            builder.Property(c => c.Status)
              .HasColumnName("status")
              .IsRequired();

            builder.Property(c => c.CreatedAt)
              .HasColumnName("created_at")
              .IsRequired();

            builder.HasOne(c => c.Users)
                .WithMany(c => c.Candidates)
                .HasForeignKey(c => c.UserId);
            builder.HasKey(c => c.UserId);

            builder.HasOne(c => c.Companies)
                .WithMany(c => c.Candidates)
                .HasForeignKey(c => c.CompaniyId);

            builder.HasOne(c => c.Accelerations)
                .WithMany(c => c.Candidates)
                .HasForeignKey(c => c.AccelerationId);

        }

    }
}
