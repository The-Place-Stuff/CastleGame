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
            if (Stats.BuildingMode)
            {
                TryPlaceBuilding();
                GetComponent<Sprite>().Enabled = true;

            }
            else
            {
                GetComponent<Sprite>().Enabled = false;
            }

            base.Update();
        }

        public void TryPlaceBuilding()
        {
            Vector2 cursorPosition = SceneManager.CurrentScene.GetGameObject<Cursor>().Position;
            float tileSize = SceneManager.CurrentScene.GetGameObject<Map>().bluprintGrid.TileSize.X;
            Position = VectorHelper.Snap(new Vector2(cursorPosition.X, cursorPosition.Y), tileSize);


            if (Input.Mouse.LeftClickRelease())
            {
                SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.PlaceTile(
                    SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.ConvertWorldCoordinatesToGridCoordinates(Position),
                    Objects.Furnace().Name
                    );
            }
        }

    }
}
