using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class LightEmitter : Component
{
    private int radius;
    private float brightness;

    public LightEmitter(int radius, float brightness) : base(false)
    {
        this.radius = radius;
        this.brightness = brightness;
    }

    public override void Initialize()
    {
        SetLight();
    }

    public void SetLight()
    {
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        Vector2 tilePosition = map.lightGrid.ConvertWorldCoordinatesToGridCoordinates(VectorHelper.Snap(GameObject.Position, 16));

        LightTile light = map.lightGrid.Tiles[tilePosition] as LightTile;

        // Loop the radius of the campfire
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                // Make sure its a circle radius
                if (MathF.Sqrt(x * x + y * y) > radius) continue;

                Vector2 lightTilePosition = new Vector2(tilePosition.X + x, tilePosition.Y + y);

                LightTile lightTile = map.lightGrid.Tiles[lightTilePosition] as LightTile;

                if (lightTile != null)
                {
                    Vector2 abs = new Vector2(Math.Abs(x), Math.Abs(y));

                    float distance = MathF.Sqrt(abs.X * abs.X + abs.Y * abs.Y);

                    float baseIntensity = brightness;
                    float minIntensity = Map.AmbientLight;

                    float multiplier = 1f - (distance / radius);
                    multiplier = MathF.Pow(multiplier, 2);
                    multiplier = MathHelper.Clamp(multiplier, 0f, 1f);

                    lightTile.LightIntensity = (minIntensity + (baseIntensity - minIntensity) * multiplier) / Map.AmbientLight;

                    //lightTile.Color = new Color(22, 21, 33) * (minIntensity + (baseIntensity - minIntensity) * multiplier);
                }
            }
        }
    }
}
