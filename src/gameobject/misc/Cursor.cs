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
    private Vector2 worldPositionBeforeScroll;

    public Cursor()
    {

    }

    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();

        animationTree.AddAnimation("assets/animation/player/cursor_interact", _ => !isDragging);
        animationTree.AddAnimation("assets/animation/player/cursor_dragging", _ => isDragging);
        
        Layer = 5;
    }

    public override void Update()
    {
        AnimationTree animationTree = GetComponent<AnimationTree>();

        if (animationTree.CurrentAnimation != null)
        {
            animationTree.CurrentAnimation.SpriteSheet.CurrentSprite.Scale = new Vector2(0.8f, 0.8f);
        }

        //Camera zoom
        int scrollValue = Input.Mouse.GetMouseWheelChange();

        if (scrollValue != 0)
        {
            float zoomAmount = 0.008f * scrollValue;
            float zoom = Math.Clamp(SceneManager.CurrentScene.Camera.Zoom + zoomAmount, 2f, 8f);

            Vector2 worldPositionBeforeZoom = ScreenToWorld(Input.Mouse.GetNewPosition());

            SceneManager.CurrentScene.Camera.Zoom = zoom;

            Vector2 worldPositionAfterZoom = ScreenToWorld(Input.Mouse.GetNewPosition());

            Vector2 zoomTranslation = worldPositionBeforeZoom - worldPositionAfterZoom;

            SceneManager.CurrentScene.Camera.Translate(zoomTranslation);
        }

        //Camera click and drag
        if (Input.Mouse.MiddleClickHold())
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

            Vector2 worldPosition = ScreenToWorld(Input.Mouse.GetNewPosition());

            Vector2 offset = new Vector2(2f, 5f);

            Position = worldPosition + offset;
        }

        base.Update();
    }

    private Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        Vector2 screenCenter = new Vector2(GraphicsConfig.SCREEN_WIDTH / 2, GraphicsConfig.SCREEN_HEIGHT / 2);
        return ((screenPosition - screenCenter) / SceneManager.CurrentScene.Camera.Zoom) + SceneManager.CurrentScene.Camera.Position;
    }

}
