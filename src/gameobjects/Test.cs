using SerpentEngine;

namespace CastleGame;
public class Test : GameObject
{
    public override void Load()
    {
        Sprite sprite = new Sprite("assets/sprites/test_tile");

        AddComponent(sprite);
    }
}
