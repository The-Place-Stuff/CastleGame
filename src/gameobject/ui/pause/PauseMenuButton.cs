using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public abstract class PauseMenuButton : GameObject
{
    private string buttonText = "";

    public PauseMenuButton(string text)
    {
        this.buttonText = text;
    }

    public override void Load()
    {
        NineSliceSprite sprite = new NineSliceSprite("assets/img/uis/nineslice");
        sprite.Size = new Vector2(128, 20);
        sprite.SetPadding(2);
        sprite.LayerOffset = -1;

        AddComponent(sprite);

        Button button = new Button(new Vector2(128, 20)); AddComponent(button);

        Text text = new Text("font/peaberry", buttonText);
        text.Color = Color.Black;
        text.LayerOffset = 10;
        text.Scale = 1f;
        text.Position = new Vector2(-text.Size.X / 2, (-text.Size.Y / 2) + 2.5f);
        AddComponent(text);

        base.Load();
    }
}
