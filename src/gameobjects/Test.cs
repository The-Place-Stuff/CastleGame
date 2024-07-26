using Microsoft.Xna.Framework;
using SerpentEngine;

namespace CastleGame;
public class Test : GameObject
{
    public override void Load()
    {
        Sprite sprite = new Sprite("assets/img/test_tile");
        Collision coll = new Collision(Position, new Vector2(16, 16));

        AddComponent(sprite);
        AddComponent(coll);

    }
}
