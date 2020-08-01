using Codenation.Challenge.Models;
using System;

namespace Codenation.Challenge.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User Users { get; set; }
        public int AccelerationId { get; set; }
        public Acceleration Accelerations { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CompaniyId { get; set; }
        public Company Companies { get; set; }
    }
}
