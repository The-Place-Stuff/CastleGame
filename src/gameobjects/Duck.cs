using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Duck : GameObject
{
    public string path = "assets/img/duck";

    public Collision collision;
    public SpriteSheet sprite;

    public Vector2 dimensions = new Vector2(16,16);
    public Duck()
    {
        collision = new Collision(Position, dimensions);
        sprite = new SpriteSheet(path, new Vector2(16), new Vector2(4, 4));

        AddComponent(collision);
        AddComponent(sprite);
    }


    public override void Update()
    {
        base.Update();
        GetInput();
    }

    public void GetInput()
    {
        if(Input.Keyboard.GetKeyPress("A"))
        {
            Position = new Vector2(Position.X - 1, Position.Y);
        }
        if (Input.Keyboard.GetKeyPress("S"))
        {
            Position = new Vector2(Position.X, Position.Y + 1);
        }
        if (Input.Keyboard.GetKeyPress("W"))
        {
            Position = new Vector2(Position.X, Position.Y - 1);
        }
        if (Input.Keyboard.GetKeyPress("D"))
        {
            Position = new Vector2(Position.X + 1, Position.Y);
        }
    }
}

