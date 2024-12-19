using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    private bool isSelecting = false;
    private Vector2 selectionStart;
    private Vector2 selectionEnd;

    private Rectangle selectionRectangle;
    private Texture2D selectionTexture;

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
        Sprite sprite = GetComponent<Sprite>();
        float baseScale = 3f;
        sprite.Scale = new Vector2(baseScale / SceneManager.CurrentScene.Camera.Zoom, baseScale / SceneManager.CurrentScene.Camera.Zoom);

        Vector2 worldPosition = Input.Mouse.GetWorldPosition();

        Vector2 offset = new Vector2(2f, 5f);

        Position = worldPosition + offset;

        if (SceneManager.CurrentScene is MainMenu) return;

        if (SceneManager.CurrentScene.Paused) return;

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

        // Box selection
        if (Input.Mouse.LeftClickHold() && SceneManager.CurrentScene is Game)
        {
            if (!isSelecting)
            {
                selectionStart = worldPosition;
                isSelecting = true;
            }

            selectionEnd = worldPosition;

            int startX = Math.Min((int)selectionStart.X, (int)selectionEnd.X);
            int startY = Math.Min((int)selectionStart.Y, (int)selectionEnd.Y);
            int endX = Math.Max((int)selectionStart.X, (int)selectionEnd.X);
            int endY = Math.Max((int)selectionStart.Y, (int)selectionEnd.Y);

            int width = endX - startX;
            int height = endY - startY;

            if (width <= 0 || height <= 0) return;

            selectionRectangle = new Rectangle(startX, startY, width, height);

            Texture2D texture = new Texture2D(SerpentGame.Instance.GraphicsDevice, width, height);

            Color[] data = new Color[width * height];

            for (int i = 0; i < data.Length; i++) data[i] = Color.Transparent;

            for (int x = 0; x < width; x++)
            {
                data[x] = Color.White; // Top border
                data[x + width * (height - 1)] = Color.White; // Bottom border
            }

            for (int y = 0; y < height; y++)
            {
                data[y * width] = Color.White; // Left border
                data[y * width + width - 1] = Color.White; // Right border
            }

            texture.SetData(data);
            selectionTexture = texture;
        }
        else
        {
            if (isSelecting)
            {
                isSelecting = false;

                Map map = SceneManager.CurrentScene.GetGameObject<Map>();
                Player player = SceneManager.CurrentScene.GetGameObject<Player>();

                foreach (Villager villager in player.Castle.Villagers)
                {
                    if (selectionRectangle.Contains(villager.Position))
                    {
                        villager.OnClick();
                    }
                }
                selectionRectangle = Rectangle.Empty;
            }
        }

        base.Update();
    }

    public override void Draw()
    {
        base.Draw();

        if (!isSelecting || selectionTexture == null) return;

        SerpentEngine.Draw.SpriteBatch.Draw(selectionTexture, selectionRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
    }
}
