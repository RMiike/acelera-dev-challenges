using System;
using System.Collections.Generic;
using Codenation.Challenge.Models;

namespace Codenation.Challenge
{
    public class SubmissionIdComparer : IEqualityComparer<Submission>
    {
        public bool Equals(Submission x, Submission y)
        {
            return x.UserId == y.UserId &&
                   x.ChallengeId == y.ChallengeId;
        }

        public int GetHashCode(Submission obj)
        {
            return (obj.UserId, obj.ChallengeId).GetHashCode();
        }
    }
}