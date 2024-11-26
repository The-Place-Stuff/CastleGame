using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;


public class WorldButton : Button
{
    public new event ClickEvent OnClick;

    public WorldButton(Vector2 size) : base(size)
    {


    }

    public override void CheckClick()
    {

        Vector2 screenPosition = Input.Mouse.GetNewPosition();
        Vector2 screenCenter = new Vector2(GraphicsConfig.SCREEN_WIDTH / 2, GraphicsConfig.SCREEN_HEIGHT / 2);
        Vector2 worldPosition = ((screenPosition - screenCenter) / SceneManager.CurrentScene.Camera.Zoom) + SceneManager.CurrentScene.Camera.Position;

        if (!Input.Mouse.LeftClick()) return;

        if (Hitbox.Contains(worldPosition))
        {
            OnClick.Invoke();
        }
    }
}
