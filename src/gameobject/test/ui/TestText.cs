using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class TestText : GameObject
    {


        public override void Load()
        {
            Layer = 10;
            Text text = new Text("assets/font/peaberry", "hello");
            Position = new Vector2(1, 50);
            text.Scale = 1;
            AddComponent(text);
            base.Load();
        }
    }
}
