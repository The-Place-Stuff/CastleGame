using SerpentEngine;

namespace CastleGame;
public class Game : Scene
{
    private TestTileGrid testTileGrid;
    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
        testTileGrid = new TestTileGrid();

        AddGameObject(testTileGrid);
    }

    public override void Begin()
    {
        Camera.Zoom = 5f;
        Camera.SetTarget(testTileGrid);
    }

    public override void End()
    {
        
    }


    public override void Update()
    {
        base.Update();
    }
}
