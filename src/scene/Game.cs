using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tira;
public class Game : Scene
{
    public static int CurrentHour = 11;

    private Map map;
    public static Cursor cursor { get; private set; }
    public Player player = new Player();
    public PlayerStateButton playerStateButton = new PlayerStateButton();
    public BuildMenu ObjectMenu = new BuildMenu();

    public BuildGrid buildGrid = new BuildGrid();

    private RenderTarget2D cursorRenderTarget = new RenderTarget2D(SerpentGame.Instance.GraphicsDevice, GraphicsConfig.SCREEN_WIDTH, GraphicsConfig.SCREEN_HEIGHT);

    private Timer timeOfDayTimer = new Timer(1);

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
        timeOfDayTimer.Loop = true;
        timeOfDayTimer.OnTimeout += UpdateTimeOfDay;
        timeOfDayTimer.Enabled = true;
    }

    private void UpdateTimeOfDay()
    {
        CurrentHour++;

        if (CurrentHour > 23) CurrentHour = 0;

        if (CurrentHour == 0)
        {
            Map.AmbientLight = 0.8f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 5)
        {
            Map.AmbientLight = 0.7f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 6)
        {
            Map.AmbientLight = 0.6f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 7)
        {
            Map.AmbientLight = 0.5f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 8)
        {
            Map.AmbientLight = 0.4f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 9)
        {
            Map.AmbientLight = 0.3f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 10)
        {
            Map.AmbientLight = 0.2f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 16)
        {
            Map.AmbientLight = 0.3f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 17)
        {
            Map.AmbientLight = 0.4f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 18)
        {
            Map.AmbientLight = 0.5f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 20)
        {
            Map.AmbientLight = 0.6f;
            UpdateLightEmitters();
        }

        if (CurrentHour == 22)
        {
            Map.AmbientLight = 0.7f;
            UpdateLightEmitters();
        }
    }

    private void UpdateLightEmitters()
    {
        foreach (Bit bit in map.bitGrid.Bits.Values)
        {
            if (bit is Campfire == false) continue;

            Campfire campfire = bit as Campfire;

            LightEmitter lightEmitter = campfire.GetComponent<LightEmitter>();
            lightEmitter.SetLight();
        }
    }

    public override void End()
    {
        
    }


    public override void Update()
    {
        timeOfDayTimer.Update();

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

        cursor.Update();

        base.Update();

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
