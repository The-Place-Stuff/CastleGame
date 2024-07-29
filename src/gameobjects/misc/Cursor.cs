using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Cursor : GameObject
    {
        Sprite sprite;

        public Cursor()
        {
            sprite = new Sprite(Registry.Path + "cursor");
            AddComponent(sprite);
        }

        public override void Update()
        {
            Position = Input.Mouse.GetNewPosition() - new Vector2(GraphicsConfig.SCREEN_WIDTH / 2, GraphicsConfig.SCREEN_HEIGHT / 2);
            Position = Position / SceneManager.CurrentScene.Camera.Zoom;
            base.Update();
        }



    }
}
