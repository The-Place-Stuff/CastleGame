using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Furnace : GameObject
{
    public override void Load()
    {
        SpriteSheet spritesheet = new SpriteSheet("assets/img/furnace", new Vector2(16, 16), new Vector2(4, 4));
        AddComponent(spritesheet);
    }
}
