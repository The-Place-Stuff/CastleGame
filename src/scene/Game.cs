using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tira;
public class Game : Scene
{
    public static int CurrentHour = 11;
    public static int CurrentMinute = 0;
    public static Cursor cursor { get; private set; }

    public Player player = new Player();
    public PlayerStateButton playerStateButton = new PlayerStateButton();
    public BuildMenu ObjectMenu = new BuildMenu();
    public BuildGrid buildGrid = new BuildGrid();

    private PauseMenu pauseMenu = new PauseMenu();

    private RenderTarget2D cursorRenderTarget = new RenderTarget2D(SerpentGame.Instance.GraphicsDevice, GraphicsConfig.SCREEN_WIDTH, GraphicsConfig.SCREEN_HEIGHT);

    private Map map;

    private Timer timeOfDayTimer = new Timer(1f);

    private bool pauseButtonPressedLastFrame = false;

    public Game() : base("Game")
    {

    }


    public override void LoadContent()
    {
        Camera.Zoom = 5f;
        Camera.UIScale = 5f;

        pauseMenu.Initialize();

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

    public override void End()
    {

    }

    private void UpdateTimeOfDay()
    {
        if (Paused) return;

        if (CurrentMinute < 59) CurrentMinute++;
        else
        {
            CurrentMinute = 0;
            CurrentHour++;
        }

        if (CurrentHour > 23) CurrentHour = 0;

        Map.AmbientLight = 0.3f * (float)Math.Cos((CurrentMinute + 60 * CurrentHour) / 229.1831f) + 0.5f;
        UpdateLightEmitters();
    }

    private void UpdateLightEmitters()
    {
        foreach (Chunk chunk in map.chunks.Values)
        {
            foreach (Bit bit in chunk.Bits.Values)
            {
                if (bit is Campfire == false) continue;

                Campfire campfire = bit as Campfire;

                LightEmitter lightEmitter = campfire.GetComponent<LightEmitter>();
                lightEmitter.SetLight();
            }
        }
    }


    public override void Update()
    {
        bool pauseButtonPressed = Input.Keyboard.GetKeyPress(Keys.Escape.ToString());

        if (pauseButtonPressed && !pauseButtonPressedLastFrame)
        {
            if (Paused) Resume();
            else Pause();
        }

        pauseButtonPressedLastFrame = pauseButtonPressed;

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

        if (Paused)
        {
            pauseMenu.Update(); 
            return;
        }

        base.Update();

    }

    public override void Pause()
    {
        base.Pause();

        UIElements.ForEach(uiElement =>
        {
           if (uiElement is PlayerStateButton)
            {
                uiElement.Enabled = false;
                return;
            }
        });
    }

    public override void Resume()
    {
        base.Resume();

        UIElements.ForEach(uiElement =>
        {
            if (uiElement is PlayerStateButton)
            {
                uiElement.Enabled = true;
                return;
            }
        });
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

        // Pause Menu
        if (Paused) {
            SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Matrix.Identity * Matrix.CreateScale(Camera.UIScale));
            SerpentEngine.Draw.SpriteBatch.Draw(SerpentEngine.Draw.Pixel, new Rectangle(0, 0, GraphicsConfig.SCREEN_WIDTH, GraphicsConfig.SCREEN_HEIGHT), new Color(0, 0, 0, 0.5f));
            pauseMenu.Draw();
            SerpentEngine.Draw.SpriteBatch.End();
        }

        SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
        SerpentEngine.Draw.SpriteBatch.Draw(cursorRenderTarget, Vector2.Zero, Color.White);
        SerpentEngine.Draw.SpriteBatch.End();
    }
}
