using Microsoft.Xna.Framework;
using SerpentEngine;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class Logo : GameObject
{
    public float Range { get; set; } = 50;

    public float Speed { get; set; } = 0.1f;

    private int direction = (int)Direction.North;
    private int counter = 0;

    public override void Load()
    {
        Sprite sprite = new Sprite("assets/img/uis/logo"); AddComponent(sprite);


        base.Load();
    }

    public override void Update()
    {

        Move();

        base.Update();
    }

    public void Move()
    {
        if(counter >= Range)
        {
            if (direction == (int)Direction.North)
            {
                direction = (int)Direction.South;
                counter = 0;

            }
            else if (direction == (int)Direction.South)
            {
                direction = (int)Direction.North;
                counter = 0;
            }
        }

        if(direction == (int)Direction.North)
        {
            Position = new Vector2(Position.X, Position.Y - Speed);
        }
        else if (direction == (int)Direction.South)
        {
            Position = new Vector2(Position.X, Position.Y + Speed);
        }

        counter++;
    }
}
