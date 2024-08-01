using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public abstract class Character : GameObject
    {
        public string Name { get; set; }

        public Vector2 direction;
        public int counter = 0;
        public bool isMoving = false;
        public Character(string name)
        {
            Name = name;
        }

        public override void Load()
        {
            Layer = 2;

            AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
            StateMachine stateMachine = CreateAndAddComponent<StateMachine>();

            animationTree.AddAnimation("assets/animation/" + Name, _ => true);

            base.Load();
        }

        public override void Update()
        {
            Random random = new Random();

            if(random.Next(1,5) == 4)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            if (isMoving)
            {
                if (counter > 6)
                {
                    counter = 0;
                    direction = new Vector2(random.Next(0, 3) - 1, random.Next(0, 3) - 1);
                    Position = Position + direction;

                }
                else
                {
                    Position = Position + direction;

                }
                counter++;
            }

            base.Update();
        }
    }
}
