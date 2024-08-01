using SerpentEngine;
using System.Diagnostics;

namespace CastleGame;
public class Game : Scene
{
    private Map map;
    private Cursor cursor;
    private Bluprint bluprint;

    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
        Camera.Zoom = 5f;

        map = new Map();
        cursor = new Cursor();
        bluprint = new Bluprint("furnace_off");

        AddGameObject(map);
        AddGameObject(cursor);
        AddGameObject(bluprint);

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
