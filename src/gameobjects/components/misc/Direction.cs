using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Direction : Component
    {
        public Direction() : base(false)
        {
            Name = "none";
        }

        public string Name { get; private set; }

        public void Set(string direction)
        {
            Name = direction;
        }

        public static Direction None()
        {
            Direction direction = new Direction();
            direction.Set("none");

            return direction;
        }
        public static Direction North()
        {
            Direction direction = new Direction();
            direction.Set("north");

            return direction;
        }
        public static Direction West()
        {
            Direction direction = new Direction();
            direction.Set("west");

            return direction;
        }
        public static Direction South()
        {
            Direction direction = new Direction();
            direction.Set("soth");

            return direction;
        }
        public static Direction East()
        {
            Direction direction = new Direction();
            direction.Set("east");

            return direction;
        }

    }
}
