using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Source.Services
{
    public static class HandleVerify
    {
        public static bool Verify(object firstValue, object secondValue)
        {
            if ((firstValue == null && secondValue == null) ||
                 (firstValue != null && secondValue != null))
                    return true;

            return false;
        }
    }
}
