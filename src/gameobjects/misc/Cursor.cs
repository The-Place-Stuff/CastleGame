using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Cursor : GameObject
{

    private bool isDragging = false;
    private Vector2 lastMousePosition;

    public Cursor()
    {

    }

    public override void Load()
    {
        Sprite sprite = new Sprite(Registry.Path + "cursor");
        AddComponent(sprite);

        Layer = 5;
    }

    public override void Update()
    {
        if(Input.Mouse.LeftClickRelease())
        {
            SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.PlaceTile(
                SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.ConvertWorldCoordinatesToGridCoordinates(Position),
                Objects.Furnace().Name
                );
        }
        //Camera zoom
        int scrollValue = Input.Mouse.GetMouseWheelChange();

        if (scrollValue != 0)
        {
            float zoomAmount = 0.01f * scrollValue;
            float zoom = Math.Clamp(SceneManager.CurrentScene.Camera.Zoom + zoomAmount, 2f, 8f);

            SceneManager.CurrentScene.Camera.Zoom = zoom;
        }

        //Camera click and drag
        if (Input.Mouse.LeftClickHold())
        {
            Vector2 currentMousePosition = Input.Mouse.GetNewPosition();

            if (!isDragging)
            {
                isDragging = true;
                lastMousePosition = currentMousePosition;
            }

            Vector2 deltaMouse = currentMousePosition - lastMousePosition;
            lastMousePosition = currentMousePosition;

            Vector2 cameraMove = -deltaMouse / SceneManager.CurrentScene.Camera.Zoom;

            SceneManager.CurrentScene.Camera.Translate(cameraMove);
        }
        else
        {
            isDragging = false;

            Vector2 screenPosition = Input.Mouse.GetNewPosition();
            Vector2 screenCenter = new Vector2(GraphicsConfig.SCREEN_WIDTH / 2, GraphicsConfig.SCREEN_HEIGHT / 2);
            Vector2 worldPosition = (screenPosition - screenCenter) / SceneManager.CurrentScene.Camera.Zoom + SceneManager.CurrentScene.Camera.Position;

            Position = worldPosition;
        }

        base.Update();

    }


}
