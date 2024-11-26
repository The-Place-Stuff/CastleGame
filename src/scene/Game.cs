using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SerpentEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tira;
public class Game : Scene
{
    private Map map;
    public static Cursor cursor { get; private set; }
    public Player player = new Player();
    public PlayerStateButton playerStateButton = new PlayerStateButton();
    public BuildMenu ObjectMenu = new BuildMenu();

    public BuildGrid buildGrid = new BuildGrid();

    private RenderTarget2D cursorRenderTarget = new RenderTarget2D(SerpentGame.Instance.GraphicsDevice, GraphicsConfig.SCREEN_WIDTH, GraphicsConfig.SCREEN_HEIGHT);

    public Game() : base("Game")
    {

    }


    public override void LoadContent()
    {
        Camera.Zoom = 5f;
        Camera.UIScale = 5f;

        map = new Map();
        AddGameObject(map);

        cursor = new Cursor();
        cursor.Load();

        AddUIElement(playerStateButton);

        AddGameObject(buildGrid);

        AddUIElement(ObjectMenu);

        AddGameObject(player);
    }

    public override void Begin()
    {
        
    }

    public override void End()
    {
        
    }


    public override void Update()
    {   
        base.Update();
        cursor.Update();

        int farthestNegativeX = 0;
        int farthestNegativeY = 0;
        int farthestPositiveX = 0;
        int farthestPositiveY = 0;

        foreach (Chunk chunk in map.chunks.Values)
        {
            if (chunk.X < farthestNegativeX) farthestNegativeX = chunk.X;
            if (chunk.X > farthestPositiveX) farthestPositiveX = chunk.X;

            if (chunk.Y < farthestNegativeY) farthestNegativeY = chunk.Y;
            if (chunk.Y > farthestPositiveY) farthestPositiveY = chunk.Y;
        }

        Camera.Position = Vector2.Clamp(Camera.Position, new Vector2(farthestNegativeX, farthestNegativeY) * 48, new Vector2(farthestPositiveX, farthestPositiveY) * 48);
    }

    public override void Draw()
    {
        SerpentGame.Instance.GraphicsDevice.SetRenderTarget(cursorRenderTarget);
        SerpentGame.Instance.GraphicsDevice.Clear(Color.Transparent);

        SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Camera.Matrix);
        cursor.Draw();
        SerpentEngine.Draw.SpriteBatch.End();

        SerpentGame.Instance.GraphicsDevice.SetRenderTarget(null);

        base.Draw();

        SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
        SerpentEngine.Draw.SpriteBatch.Draw(cursorRenderTarget, Vector2.Zero, Color.White);
        SerpentEngine.Draw.SpriteBatch.End();
    }
}
