using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Landmark : Object
{
    public int Radius { get; private set; }

    public Landmark(string name, int radius, ObjectProperties objectProperties) : base(name, objectProperties)
    {
        Radius = radius;
    }
}
