using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SerpentEngine;
using System.Collections.Generic;

namespace CastleGame
{
    public class Main : SerpentGame
    {
        public Main() : base("Castle Game")
        {
            GraphicsConfig.SCREEN_WIDTH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            GraphicsConfig.SCREEN_HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            GraphicsConfig.FULLSCREEN = true;
            GraphicsConfig.VSYNC = true;
            GraphicsConfig.FRAMERATE = 60;
            GraphicsConfig.Apply();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            // Scene loading
            Scene game = new Game();

            SceneManager.AddScene(game);

            SceneManager.SetCurrentScene(game);

            // Debug GUIs
            ImGuiDrawable debugGui = new DebugGui();

            ImGuiManager.AddGuiDrawable(debugGui);

            List<GameObjectDebugGui> gameObjectDebugGuis = new List<GameObjectDebugGui>();

            foreach (GameObject obj in game.GameObjects)
            {
                gameObjectDebugGuis.Add(new GameObjectDebugGui(obj));
                ImGuiManager.AddGuiDrawable(new GameObjectDebugGui(obj));

            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
