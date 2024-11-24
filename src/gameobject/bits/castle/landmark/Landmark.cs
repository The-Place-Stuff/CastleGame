using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Landmark : Bit
{
    public int Radius { get; private set; }

    public Landmark(string name, int radius, BitProperties bitProperties) : base(name, bitProperties)
    {
        Radius = radius;
    }
}
