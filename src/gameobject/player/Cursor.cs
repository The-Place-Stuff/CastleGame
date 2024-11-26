using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class Cursor : GameObject
{
    public Cursor()
    {

    }

    public override void Load()
    {
        Layer = 5;

        Sprite sprite = new Sprite("assets/img/cursor");
        sprite.Scale = new Vector2(0.8f, 0.8f);

        AddComponent(sprite);
    }

    public override void Update()
    {
        //Camera zoom
        int scrollValue = Input.Mouse.GetMouseWheelChange();

        if (scrollValue != 0)
        {
            float zoomAmount = 0.008f * scrollValue;
            float zoom = Math.Clamp(SceneManager.CurrentScene.Camera.Zoom + zoomAmount, 3f, 6f);

            Vector2 worldPositionBeforeZoom = Input.Mouse.GetWorldPosition();

            SceneManager.CurrentScene.Camera.Zoom = zoom;

            Vector2 worldPositionAfterZoom = Input.Mouse.GetWorldPosition();

            Vector2 zoomTranslation = worldPositionBeforeZoom - worldPositionAfterZoom;

            SceneManager.CurrentScene.Camera.Translate(zoomTranslation);
        }

        Vector2 worldPosition = Input.Mouse.GetWorldPosition();

        Vector2 offset = new Vector2(2f, 5f);

        Position = worldPosition + offset;

        base.Update();
    }

}
