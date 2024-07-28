using SerpentEngine;
using System.Diagnostics;

namespace CastleGame;
public class Game : Scene
{
    private Map grid;
    private Test test;
    private Cursor cursor;

    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
        grid = new Map();

        AddGameObject(grid);

        test = new Test();
        cursor = new Cursor();
        AddGameObject(cursor);

    }

    public override void Begin()
    {
        Camera.Zoom = 5f;
    }

    public override void End()
    {
        
    }


    public override void Update()
    {
        base.Update();

    }
}
