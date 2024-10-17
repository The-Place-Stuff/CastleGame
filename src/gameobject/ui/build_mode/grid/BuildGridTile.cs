using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class BuildGridTile : Tile
{
    public BuildGridTile() : base("build_grid_tile")
    {
    }

    public override void Load()
    {
        Sprite sprite = new Sprite("assets/img/null");

        Texture2D texture = new Texture2D(SerpentGame.Instance.GraphicsDevice, 16 * 128, 16 * 128);
        Color[] data = new Color[texture.Width * texture.Height];

        for (int y = 0; y < texture.Height; y++)
        {
            for (int x = 0; x < texture.Width; x++)
            {
                if (x % 32 == 0 || y % 32 == 0)
                {
                    data[y * texture.Width + x] = Color.White * 0.4f;
                }
                else data[y * texture.Width + x] = Color.Transparent;
            }
        }

        texture.SetData(data);

        sprite.texture2d = texture;

        sprite.Scale = new Vector2(0.5f, 0.5f);

        sprite.Offset = new Vector2(7.8f, 7.8f);

        AddComponent(sprite);

        Layer = 2;
    }
}
