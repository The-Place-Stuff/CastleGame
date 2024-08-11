using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class GoTask : Task
    {
        public GoTask(GameObject obj) : base(obj)
        {

        }

        public GoTask(Vector2 position) : base(position)
        {

        }

        public override void Start()
        {
            Character.GetComponent<StateMachine>().SetState(CharacterStates.Wandering.Name);
            Character.GetComponent<PatrolMovementAI>().Path = Target.Position;

            base.Start();
        }
    }
}
