using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class States : Registry
{
    public static readonly GameObjectState On = new BooleanState("on");

    public static readonly GameObjectState Off = new BooleanState("off");

}

