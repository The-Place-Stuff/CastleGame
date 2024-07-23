using SerpentEngine;

namespace CastleGame;
public class Game : Scene
{
    public Test Test { get; private set; }

    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
        Test = new Test();

        AddGameObject(Test);
    }

    public override void Begin()
    {
        Camera.Zoom = 5f;
        Camera.SetTarget(Test);
    }

    public override void End()
    {
        
    }


    public override void Update()
    {
        base.Update();
    }
}
