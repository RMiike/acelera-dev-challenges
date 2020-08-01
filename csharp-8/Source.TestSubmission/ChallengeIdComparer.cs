using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
    public class ChallengeIdComparer : IEqualityComparer<Models.Challenge>
    {
        public bool Equals(Models.Challenge x, Models.Challenge y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Models.Challenge obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}