using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame.src.states
{
    public class DirectionState : GameObjectState
    {
        public DirectionState(string direction) : base(direction)
        {
        }

        public override void Enter()
        {
            Debug.WriteLine("Entering direction state..");
        }

        public override void Update()
        {

        }
    }
}
