using System;
using System.Collections.Generic;
using Codenation.Challenge.Models;

namespace Codenation.Challenge
{
    public class AccelerationIdComparer : IEqualityComparer<Acceleration>
    {
        public bool Equals(Acceleration x, Acceleration y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Acceleration obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}