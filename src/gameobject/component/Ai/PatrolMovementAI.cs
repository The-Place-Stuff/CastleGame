using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class PatrolMovementAI : Component
{
    public Vector2 Path = new Vector2();
    public PatrolMovementAI() : base(false)
    {
    }

    public void ChangePath(Vector2 wp)
    {
        Path = wp;
    }

    public override void Update()
    {
        if (Path == Vector2.Zero) return;

        Character character = GameObject as Character;

        Vector2 dir = Path - character.Position;

        if (dir.Length() < 4) return;

        dir.Normalize();

        if (dir.X > 0)
        {
            character.CurrentDirection = new Vector2(1, 0);
        }

        if (dir.X < 0)
        {
            character.CurrentDirection = new Vector2(-1, 0);
        }

        Vector2 d = new Vector2((int)Math.Ceiling(dir.X), (int)Math.Ceiling(dir.Y));

        character.Position += dir * character.Properties.Speed * (float)Main.DeltaTime;
    }
}
