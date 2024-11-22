using Microsoft.Xna.Framework;
using SerpentEngine;

namespace CastleGame;

public class LightTile : Tile
{
    public LightTile() : base("light")
    {
    }

    public override void Load()
    {
        Light light = new Light();

        light.Color = Color.Black * 0.35f;

        AddComponent(light);
    }
}
