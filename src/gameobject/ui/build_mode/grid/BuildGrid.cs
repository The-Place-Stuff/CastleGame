using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class BuildGrid : GameObject
{
    public override void Load()
    {
        Enabled = false;

        TileSet tileSet = new TileSet();
        tileSet.Add("build_grid_tile", () => new BuildGridTile());

        TileGrid grid = new TileGrid(new Vector2(1024, 1024));

        grid.AddTileSet(tileSet);

        AddComponent(grid);

        Layer = 2;

        for (int x = -5; x < 5; x++)
        {
            for (int y = -5; y < 5; y++)
            {
                grid.PlaceTile(new Vector2(x, y), "build_grid_tile");
            }
        }
    }
}
