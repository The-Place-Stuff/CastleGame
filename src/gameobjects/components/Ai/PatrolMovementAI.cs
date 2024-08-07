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
        public Vector2 Path = new Vector2();

        public void ChangePath(Vector2 wp)
        {
            Path = wp;
        }

        public override void Move(Character character)
        {
            if (Path == Vector2.Zero) return;

            var dir = Path - character.Position;

            if (dir.Length() > 4)
            {
                dir.Normalize();
                Vector2 d = new Vector2((int)Math.Ceiling(dir.X), (int)Math.Ceiling(dir.Y));

                character.CurrentDirction = d;
                character.Position += dir * character.Speed * (float)Main.GameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
