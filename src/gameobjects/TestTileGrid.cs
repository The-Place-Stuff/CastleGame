using Microsoft.Xna.Framework;
using SerpentEngine;

namespace CastleGame;
public class TestTileGrid : GameObject
{
    public override void Load()
    {
        TileSet tileSet = new TileSet();

        tileSet.AddFromSprite("test_tile", "assets/img/test_tile");

        TileGrid tileGrid = new TileGrid(new Vector2(16, 16));

        tileGrid.AddTileSet(tileSet);

        AddComponent(tileGrid);

        tileGrid.PlaceTiles(new Vector2(-20, -10), new Vector2(20, 10), "test_tile");
    }
}
