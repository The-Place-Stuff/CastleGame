using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class TaskMenu : GameObject
{


    public override void Load()
    {
        Layer = 10;
        NineSliceSprite nineSliceSprite = new NineSliceSprite("assets/img/nineslice");
        nineSliceSprite.Size = new Vector2(24, 54);
        nineSliceSprite.SetPadding(2);
        AddComponent(nineSliceSprite);


    }
}

