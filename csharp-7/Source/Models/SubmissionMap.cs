using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codenation.Challenge.Models
{
    public class SubmissionMap : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.ToTable("submission");

            builder.HasKey(c => c.UserId);


            builder.Property(s => s.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(s => s.ChallengeId)
                .HasColumnName("challenge_id")
                .IsRequired();

            builder.Property(s => s.Score)
                .HasColumnName("score")
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.HasOne(s => s.User)
                .WithMany(s => s.Submissions)
                .HasForeignKey(s => s.UserId);


            builder.HasOne(s => s.Challenges)
                .WithMany(s => s.Submissions)
                .HasForeignKey(s => s.ChallengeId);

        }
    }
}
