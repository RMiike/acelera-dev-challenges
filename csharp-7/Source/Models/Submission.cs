using System;

namespace Codenation.Challenge.Models
{
    public class Submission
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ChallengeId { get; set; }
        public Challenge Challenges { get; set; }
        public decimal Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
