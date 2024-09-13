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
    public ObjectHitTransformation() : base()
    {
    }

    public override Sprite Transform(Sprite sprite)
    {
        sprite.Scale += new Vector2(0.1f, 0.1f);
        return sprite;  
    }
}
