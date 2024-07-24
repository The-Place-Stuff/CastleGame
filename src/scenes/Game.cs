using SerpentEngine;

namespace CastleGame;
public class Game : Scene
{
    private Furnace furnace;
    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
        furnace = new Furnace();

        AddGameObject(furnace);
    }

    public override void Begin()
    {
        Camera.Zoom = 5f;
        Camera.SetTarget(furnace);
    }

    public override void End()
    {
        
    }


    public override void Update()
    {
        base.Update();
    }
}
