using Microsoft.Xna.Framework;
using SerpentEngine;
using System;

namespace Tira;
public class Campfire : Landmark
{
    private bool tilesLit = false;

    public Campfire(string name, int radius, BitProperties bitProperties) : base(name, radius, bitProperties)
    {
    }

    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation), _ =>  true);

        base.Load();
    }

    public override void Update()
    {
        base.Update();

        if (tilesLit) return;

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        Vector2 tilePosition = VectorHelper.Snap(Position, 16);

        LightTile light = map.lightGrid.GetTileFromGridCoordinates(tilePosition) as LightTile;
        light.Color = Color.Black * 0.1f;

        int radius = 8;

        // Loop the radius of the campfire
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                // Make sure its a circle radius
                if (MathF.Sqrt(x * x + y * y) > radius) continue;

                Vector2 lightTilePosition = new Vector2(tilePosition.X + x, tilePosition.Y + y);

                LightTile lightTile = map.lightGrid.GetTileFromGridCoordinates(lightTilePosition) as LightTile;

                if (lightTile != null)
                {
                    Vector2 abs = new Vector2(Math.Abs(x), Math.Abs(y));

                    float distance = MathF.Sqrt(abs.X * abs.X + abs.Y * abs.Y);

                    float baseIntensity = 0.1f;
                    float minIntensity = 0.5f;

                    float multiplier = 1f - (distance / radius);
                    multiplier = MathF.Pow(multiplier, 2);
                    multiplier = MathHelper.Clamp(multiplier, 0f, 1f);

                    lightTile.Color = Color.Black * (minIntensity + (baseIntensity - minIntensity) * multiplier);
                }
            }
        }

        tilesLit = true;
    }


}
