using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class States : Registry
    {
        public static readonly GameObjectState On = new BooleanState("on");

        public static readonly GameObjectState Off = new BooleanState("off");

        public static readonly GameObjectState North = new DirectionState("north");

        public static readonly GameObjectState South = new DirectionState("south");

        public static readonly GameObjectState East = new DirectionState("east");

        public static readonly GameObjectState West = new DirectionState("west");

        public static readonly GameObjectState Idle = new DirectionState("idle");

    }
}
