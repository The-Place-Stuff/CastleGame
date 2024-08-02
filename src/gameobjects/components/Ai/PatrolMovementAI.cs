using CastleGame.src;
using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class PatrolMovementAI : MovementAI
    {
        public readonly List<Vector2> Path = new();
        private int Current;

        public void AddWaypoint(Vector2 wp)
        {
            Path.Add(wp);
        }

        public override void Move(Character character)
        {
            if (Path.Count < 1) return;

            var dir = Path[Current] - character.Position;

            if (dir.Length() > 4)
            {
                dir.Normalize();
                Vector2 d = new Vector2((int)Math.Round(dir.X), (int)Math.Round(dir.Y));

                character.Direction = d;
                character.Position += dir * character.Speed * (float)Main.GameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Current = (Current + 1) % Path.Count;
            }
        }
    }
}
