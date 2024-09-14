using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Base : Component
{
    public int Radius { get; set; } = 0;

    public Base() : base(false)
    {
    }


}
