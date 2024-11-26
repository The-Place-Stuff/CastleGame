using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira
{
    public class DropProperties
    {
        public DropProperties(int count, float chance)
        {
            Count = count;
            Chance = chance;
        }
        public int Count { get; set; }
        public float Chance { get; set; }

    }
}
