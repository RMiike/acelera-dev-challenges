using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codenation.Challenge.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        public List<Candidate> Candidates { get; set; }
    }
}