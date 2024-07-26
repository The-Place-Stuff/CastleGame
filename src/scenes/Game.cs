using SerpentEngine;
using System.Diagnostics;

namespace CastleGame;
public class Game : Scene
{
<<<<<<< Updated upstream
    private TestTileGrid testTileGrid;
    public Furnace Furnace;
    public Duck duck;
=======
    private Furnace furnace;
    private Test test;

>>>>>>> Stashed changes
    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
<<<<<<< Updated upstream
        testTileGrid = new TestTileGrid();
        Furnace = new Furnace();
        duck = new Duck();

        AddGameObject(testTileGrid);
        AddGameObject(Furnace);
        AddGameObject(duck);
=======
        furnace = new Furnace();
        test = new Test();
        
>>>>>>> Stashed changes

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
