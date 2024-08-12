using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CastleGame
{
    public class Bluprint : Object
    {
        public string currentBuilding;
        public Bluprint(string name) : base(name)
        {

        }

        public override void Load()
        {
            Color c = Color.CornflowerBlue;
            c.A = 150;
            Sprite sprite = new Sprite(Objects.GetPath(Name));
            sprite.Color = c;
            AddComponent(sprite);
        }


        public override void Update()
        {
            Sprite sprite = GetComponent<Sprite>();

            if (Player.BuildingMode)
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

            Vector2 cursorPosition = Game.cursor.Position;

            float tileSize = map.bluprintGrid.TileSize.X;

            Position = VectorHelper.Snap(new Vector2(cursorPosition.X, cursorPosition.Y), tileSize);

            if (Input.Mouse.LeftClickRelease())
            {
                map.objectGrid.PlaceTile(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(Position), Objects.Furnace().Name);
            }
        }

    }
}
