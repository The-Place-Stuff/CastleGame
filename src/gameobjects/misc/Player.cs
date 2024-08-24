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

    public static TaskMenu TaskMenu;

    public static string CurrentBluprint = Objects.Workbench().Name;

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
        if (BuildingMode)
        {
            TryPlaceBuilding();
            sprite.Enabled = true;

        }
        else
        {
            sprite.Enabled = false;
        }

        if(Input.Mouse.RightClick())
        {
            if (TaskMenu == null)
            {
                TaskMenu taskMenu = new TaskMenu();
                taskMenu.Position = Game.cursor.Position;

                TaskMenu = taskMenu;

                SceneManager.CurrentScene.AddGameObject(taskMenu);
            }
            else
            {
                SceneManager.CurrentScene.Remove(TaskMenu);
                TaskMenu taskMenu = new TaskMenu();
                taskMenu.Position = Game.cursor.Position;

                TaskMenu = taskMenu;

                SceneManager.CurrentScene.AddGameObject(taskMenu);
            }
        }
        if(Input.Mouse.LeftClick())
        {
            SceneManager.CurrentScene.Remove(TaskMenu);

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

            map.objectGrid.PlaceTile(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(Position), Objects.Blueprint().Name);
            map.objectGrid.GetTileFromWorldCoordinates(Position).Name = CurrentBluprint;
            map.objectGrid.GetTileFromWorldCoordinates(Position).RemoveComponent(map.objectGrid.GetTileFromWorldCoordinates(Position).GetComponent<Sprite>());
            map.objectGrid.GetTileFromWorldCoordinates(Position).Load();

            //DebugGui.Log(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(cursorPosition) + " Placed");


        }
    }

}
