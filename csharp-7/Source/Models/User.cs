using System;
using System.Collections.Generic;

namespace Codenation.Challenge.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Submission> Submissions { get; set; }
        public List<Candidate> Candidates { get; set; }
    }
}
