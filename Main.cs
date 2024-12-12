using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SerpentEngine;
using System.Collections.Generic;

namespace Tira
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
            GraphicsConfig.BACKGROUND_COLOR = new Color(142, 205, 5);
            IsMouseVisible = false;
            GraphicsConfig.Apply();

            Content.RootDirectory = "assets";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Bits.RegisterObjects();
            Characters.RegisterCharacters();
            Items.RegisterItems();
            ItemRecipes.RegisterRecipes();

            SerpentGame.ImGuiManager.AddGuiDrawable(new CastleGameDebugGui());

            base.LoadContent();


            // Scene loading
            Scene main = new MainMenu();            

            SceneManager.SetCurrentScene(main);

            // Debug GUIs
            ImGuiDrawable debugGui = new DebugGui();

            ImGuiManager.AddGuiDrawable(debugGui);
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
