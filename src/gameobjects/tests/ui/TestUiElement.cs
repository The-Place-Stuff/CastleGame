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
        Sprite sprite = new Sprite("assets/img/objects/tent");

        AddComponent(sprite);

        Position = new Vector2(132, 200);
        sprite.Scale = new Vector2(5f, 5f);
    }
}
