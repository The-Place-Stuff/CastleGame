using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Test : GameObject
{
    public override void Load()
    {
        Sprite sprite = new Sprite("assets/sprites/test_tile");

        AddComponent(sprite);
    }
}
