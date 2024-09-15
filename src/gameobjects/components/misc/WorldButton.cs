using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;


public class WorldButton : Button
{
    public new event ClickEvent OnClick;

    public WorldButton(Vector2 size) : base(size)
    {


    }

    public override void CheckClick()
    {
        Rectangle box = new Rectangle((int)GameObject.Position.X, (int)GameObject.Position.Y, (int)Size.X, (int)Size.Y);

        Vector2 position = Game.cursor.Position;
        if (!Input.Mouse.LeftClick()) return;

        if (box.Contains(position))
        {
            OnClick.Invoke();
        }
    }
}
