using Microsoft.Xna.Framework;
using SerpentEngine;

namespace CastleGame;

public class LightTile : Tile
{
    public LightTile() : base("assets/img/null", "light")
    {
        texture2d = SerpentEngine.Draw.Pixel;
        Scale = new Vector2(16, 16);
        Offset = new Vector2(-8, -8);
        Color = Color.Black * 0.35f;
    }
}
