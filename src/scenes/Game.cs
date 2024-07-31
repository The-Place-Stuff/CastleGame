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
        Camera.Zoom = 5f;

        grid = new Map();

        AddGameObject(grid);

        test = new Test();
        cursor = new Cursor();
        AddGameObject(cursor);

        TestUiElement testUiElement = new TestUiElement();

        AddUIElement(testUiElement);

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

    }
}
