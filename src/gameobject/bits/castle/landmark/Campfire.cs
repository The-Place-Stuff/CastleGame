using Microsoft.Xna.Framework;
using SerpentEngine;
using System;

namespace CastleGame;
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

        Light light = map.lightGrid.GetTileFromGridCoordinates(tilePosition).GetComponent<Light>();
        light.Color = Color.Black * 0.1f;

        // Loop the radius of the campfire
        for (int x = -5; x <= 5; x++)
        {
            for (int y = -5; y <= 5; y++)
            {
                // Make sure its a circle radius
                if (MathF.Sqrt(x * x + y * y) > 5) continue;


                Vector2 lightTilePosition = new Vector2(tilePosition.X + x, tilePosition.Y + y);

                LightTile lightTile = map.lightGrid.GetTileFromGridCoordinates(lightTilePosition) as LightTile;

                if (lightTile != null)
                {
                    Vector2 abs = new Vector2(Math.Abs(x), Math.Abs(y));

                    float distance = MathF.Sqrt(abs.X * abs.X + abs.Y * abs.Y);

                    // Farther away from the center, the heigher the multiplier
                    float multiplier = 0.8f + (distance / 5);

                    lightTile.GetComponent<Light>().Color = Color.Black * MathHelper.Clamp(0.15f * multiplier, 0.1f, 0.35f);
                }
            }
        }

        tilesLit = true;
    }


}
