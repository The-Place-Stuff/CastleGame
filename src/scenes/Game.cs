using SerpentEngine;
using System.Diagnostics;

namespace CastleGame;
public class Game : Scene
{
    private Grid grid;
    private Test test;

    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
        grid = new Grid();

        AddGameObject(grid);

        test = new Test();

        AddGameObject(test);


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
