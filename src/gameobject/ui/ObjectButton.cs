using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class ObjectButton : GameObject
{
    public Object Object;
    public ObjectButton(Object obj)
    {
        Object = obj;
    }


    public override void Load()
    {
        Sprite sprite = new Sprite(Objects.GetPath(Object.Name)); sprite.Scale = new Vector2(1f,1f); AddComponent(sprite);
        Button button = new Button(new Vector2(18, 18)); AddComponent(button);

        button.OnClick += Onclick;

        base.Load();
    }

    public void Onclick()
    {

    }
}
