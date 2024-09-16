using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SerpentEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace CastleGame;
public class Game : Scene
{
    private Map map;
    public static Cursor cursor { get; private set; }
    private Blueprint blueprint;
    public List<Character> characters = new List<Character>();
    public Player player = new Player();
    public PlayerStateButton CastleGameButton = new PlayerStateButton();

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

        characters.Add(Characters.Villager());


        foreach (Character character in characters)
        {
            AddGameObject(character);

        }

        blueprint = new Blueprint("furnace_off");

        AddGameObject(player);
        AddUIElement(CastleGameButton);

        

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
