using Microsoft.Xna.Framework;
using SerpentEngine;

namespace Tira;

public class LightTile : Tile
{
    public float LightIntensity = 1f;

    public LightTile() : base("assets/img/null", "light")
    {
        texture2d = SerpentEngine.Draw.Pixel;

        Scale = new Vector2(16, 16);

        Offset = new Vector2(-8, -8);
    }

    public override void Draw()
    {
        SerpentEngine.Draw.SpriteBatch.Draw(texture2d, Position + Offset, new Rectangle((int)Coordinates.X, (int)Coordinates.Y, texture2d.Width, texture2d.Height), (new Color(22, 21, 33) * Map.AmbientLight) * LightIntensity, Rotation, new Vector2(texture2d.Width / 2, texture2d.Height / 2), Scale, Effect, Layer * 0.001f);
    }
}
