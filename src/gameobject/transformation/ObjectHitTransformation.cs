using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class ObjectHitTransformation : Transformation
{
    private bool growing = true;
    public ObjectHitTransformation() : base()
    {
    }

    public override Sprite Transform(Sprite sprite)
    {
        Vector2 scale = Vector2.One;
        Vector2 max = new Vector2(1.5f, 1.5f);

        if (sprite.Scale == scale)
        {
            growing = true;
        }
        if (sprite.Scale.X > max.X)
        {
            growing = false;
        }
        if (growing)
        {
            sprite.Scale += new Vector2(0.1f, 0.1f);

        }
        else
        {
            sprite.Scale -= new Vector2(0.1f, 0.1f);

        }

        return sprite;
    }

    public override Sprite Reset(Sprite sprite)
    {
        sprite.Scale = Vector2.One;
        return sprite;
    }
}
