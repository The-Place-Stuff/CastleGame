using SerpentEngine;
using System.Diagnostics;

namespace CastleGame;
public class Game : Scene
{
    private Grid grid;
    private AnimationTest animationTest;

    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
        grid = new Grid();

        AddGameObject(grid);

        animationTest = new AnimationTest();

        AddGameObject(animationTest);


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
