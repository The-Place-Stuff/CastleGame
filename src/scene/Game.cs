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
    public PlayerStateButton playerStateButton = new PlayerStateButton();
    public BuildMenu ObjectMenu = new BuildMenu();

    private Texture2D gridlines;

    private RenderTarget2D cursorRenderTarget = new RenderTarget2D(SerpentGame.Instance.GraphicsDevice, GraphicsConfig.SCREEN_WIDTH, GraphicsConfig.SCREEN_HEIGHT);

    public Game() : base("Game")
    {

    }

    public Texture2D Gridlines()
    {
        Texture2D texture = new Texture2D(SerpentGame.Instance.GraphicsDevice, 16 * 255, 16 * 255);
        Color[] data = new Color[texture.Width * texture.Height];

        for (int y = 0; y < texture.Height; y++)
        {
            for (int x = 0; x < texture.Width; x++)
            {
                if (x % 16 == 0 || y % 16 == 0)
                {
                    data[y * texture.Width + x] = Color.Gray;
                    data[y * texture.Width + x].A = 150;

                } else data[y * texture.Width + x] = Color.Transparent;
            }
        }

        texture.SetData(data);
        return texture;
    }

    public override void LoadContent()
    {
        gridlines = Gridlines();
        Camera.Zoom = 5f;
        Camera.UIScale = 5f;

        map = new Map();
        AddGameObject(map);

        cursor = new Cursor();
        cursor.Load();


        blueprint = new Blueprint("furnace_off");

        
        AddUIElement(playerStateButton);
        AddUIElement(ObjectMenu);

        AddGameObject(player);

        characters.Add(Characters.Villager());


        foreach (Character character in characters)
        {
            AddGameObject(character);

        }


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

        Camera.Position = Vector2.Clamp(Camera.Position, new Vector2(-1700, -1700), new Vector2(1700, 1700));
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

        SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Camera.Matrix);
        SerpentEngine.Draw.SpriteBatch.Draw(gridlines, new Vector2(-255 * 8, -255 * 8), Color.White);
        SerpentEngine.Draw.SpriteBatch.End();

        SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
        SerpentEngine.Draw.SpriteBatch.Draw(cursorRenderTarget, Vector2.Zero, Color.White);
        SerpentEngine.Draw.SpriteBatch.End();
    }
}
