using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class PressStartLabel : GameObject
{

    public override void Load()
    {

        Text text = new Text("font/peaberry", "Press Space to start"); AddComponent(text);
        text.Scale = 0.7f;
        Position = Position + new Vector2(-60, 40);

        base.Load();

        
    }
}
