using System;
using System.Collections.Generic;
using Codenation.Challenge.Models;

namespace Codenation.Challenge
{
    public class CandidateIdComparer : IEqualityComparer<Candidate>
    {
        public bool Equals(Candidate x, Candidate y)
        {
            return x.UserId == y.UserId &&
                   x.AccelerationId == y.AccelerationId &&
                   x.CompanyId == y.CompanyId;
        }

        public int GetHashCode(Candidate obj)
        {
            return (obj.UserId, obj.AccelerationId, obj.CompanyId).GetHashCode();
        }
    }
}