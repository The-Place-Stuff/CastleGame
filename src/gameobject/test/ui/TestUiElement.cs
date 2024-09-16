using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class TestUiElement : GameObject
{
    public override void Load()
    {
        NineSliceSprite sprite = new NineSliceSprite("assets/img/nineslice");
        sprite.Size = new Vector2(64, 128);

        AddComponent(sprite);

        Position = new Vector2(0, 50);
    }
}
