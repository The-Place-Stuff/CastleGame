using Microsoft.Xna.Framework;
using SerpentEngine;

namespace CastleGame;
public class Grid : GameObject
{
    public override void Load()
    {
        TileSet tileSet = new TileSet();

        tileSet.AddFromSprite("grass", "assets/img/grass");

        TileGrid tileGrid = new TileGrid(new Vector2(16, 16));

        tileGrid.AddTileSet(tileSet);

        AddComponent(tileGrid);

        tileGrid.PlaceTiles(new Vector2(-20, -10), new Vector2(20, 10), "grass");
    }
}
