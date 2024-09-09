using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class CastleGameButton : GameObject
{

    public override void Load()
    {

        Sprite sprite = new Sprite("assets/img/uis/interact_mode");
        AddComponent(sprite);

        Position = new Vector2(48, (GraphicsConfig.SCREEN_HEIGHT / 5) - 32);

        Button button = new Button(new Vector2(16, 16));
        AddComponent(button);

        button.OnClick += OnClick;
    }


    public void OnClick()
    {
        DebugGui.Log("Clicked");
    }
}
