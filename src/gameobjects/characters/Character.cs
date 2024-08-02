using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public abstract class Character : GameObject
    {
        public string Name { get; set; }

        public float Speed { get; set; }

        public Vector2 Direction { get; set; }

        public Character(string name)
        {
            Name = name;
            Speed = 20;
        }

        public override void Load()
        {
            Layer = 2;

            AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
            StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
            PatrolMovementAI movementAI = CreateAndAddComponent<PatrolMovementAI>();

            GetComponent<PatrolMovementAI>().AddWaypoint(new Vector2(-100, 100));
            GetComponent<PatrolMovementAI>().AddWaypoint(new Vector2(-0, 100));

            GetComponent<StateMachine>().AddState(States.East);
            GetComponent<StateMachine>().AddState(States.West);
            GetComponent<StateMachine>().AddState(States.Idle);


            GetComponent<StateMachine>().SetState(States.Idle.Name);

            animationTree.AddAnimation("assets/animation/" + Name + "_idle", _ => GetComponent<StateMachine>().CurrentState.Name == "idle");
            animationTree.AddAnimation("assets/animation/" + Name + "_east", _ => GetComponent<StateMachine>().CurrentState.Name == "east");
            animationTree.AddAnimation("assets/animation/" + Name + "_west", _ => GetComponent<StateMachine>().CurrentState.Name == "west");

            base.Load();
        }

        public virtual void OnDestinationArrived(Vector2 position)
        {

        }

        public override void Update()
        {
            GetComponent<PatrolMovementAI>().Move(this);
            UpdateDirection();


            base.Update();
        }

        public virtual void UpdateDirection()
        {
            if(Direction.X >= 1)
            {
                GetComponent<StateMachine>().SetState(States.East.Name);
            }
            if (Direction.X <= -1)
            {
                GetComponent<StateMachine>().SetState(States.West.Name);

            }
            if (Direction.X == 0)
            {
                GetComponent<StateMachine>().SetState(States.Idle.Name);

            }
        }
    }
}
