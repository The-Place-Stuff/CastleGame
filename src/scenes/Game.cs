using SerpentEngine;

namespace CastleGame;
public class Game : Scene
{
    private TestTileGrid testTileGrid;
    public Furnace Furnace;
    public Duck duck;
    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
        testTileGrid = new TestTileGrid();
        Furnace = new Furnace();
        duck = new Duck();

        AddGameObject(testTileGrid);
        AddGameObject(Furnace);
        AddGameObject(duck);

    }

    public override void Begin()
    {
        Camera.Zoom = 5f;
        Camera.SetTarget(duck);
    }

    public override void End()
    {
        
    }


    public override void Update()
    {
        base.Update();

    }
}
