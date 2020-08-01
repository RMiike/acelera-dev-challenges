using System;
using System.Collections.Generic;
using Codenation.Challenge.Models;

namespace Codenation.Challenge
{
    public class CompanyIdComparer : IEqualityComparer<Company>
    {
        public bool Equals(Company x, Company y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Company obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}