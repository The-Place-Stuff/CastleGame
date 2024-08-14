using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Player : GameObject
{
    public static bool BuildingMode = false;

    public static string CurrentBluprint = Objects.Stockpile().Name;

    public override void Load()
    {

        Inventory inventory = CreateAndAddComponent<Inventory>();
        Color c = Color.CornflowerBlue;
        c.A = 150;
        Sprite sprite = new Sprite(Objects.GetPath(CurrentBluprint)); AddComponent(sprite);
        sprite.Color = c;
        base.Load();
    }




    public override void Update()
    {
        Layer = 2;
        Sprite sprite = GetComponent<Sprite>();
        DebugGui.Log(Position.ToString());
        if (BuildingMode)
        {
            TryPlaceBuilding();
            sprite.Enabled = true;

        }
        else
        {
            sprite.Enabled = false;
        }

        base.Update();
    }

    public void TryPlaceBuilding()
    {
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        Scene scene = SceneManager.CurrentScene;

        Vector2 cursorPosition = Game.cursor.Position;

        float tileSize = map.bluprintGrid.TileSize.X;

        Position = VectorHelper.Snap(new Vector2(cursorPosition.X, cursorPosition.Y), tileSize);
        if (Input.Mouse.LeftClickRelease())
        {
            Blueprint bluprint = new Blueprint(CurrentBluprint);
            bluprint.Position = Position;
            scene.AddGameObject(bluprint);
        }
    }

}
