using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing21Vek.Utilities
{
    public static class RandomGenerator
    {
        public static string GetEmail()
        {
            Random random = new Random();

            var str = random.NextInt64().ToString();
            return "test" + str + "@test.com";
        }
    }
}
