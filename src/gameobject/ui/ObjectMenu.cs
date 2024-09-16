using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class ObjectMenu : GameObject
{
    public List<UiElementGroup> uis = new List<UiElementGroup>();
    public override void Load()
    {
        NineSliceSprite sprite = new NineSliceSprite("assets/img/uis/nineslice");
        sprite.Size = new Vector2(200, 64);
        sprite.SetPadding(2);
        AddComponent(sprite);

        Position = new Vector2(40, 142);
        base.Load();
    }
}
