using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Tool : Item
    {
        public Tool(string name) : base(name)
        {

        }

        public static new Tool Empty()
        {
            return new Tool("");
        }
    }
}
