using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class ToolHitTransformation : Transformation
{
    public ToolHitTransformation() : base()
    {
    }

    public override Sprite Transform(Sprite sprite)
    {
        sprite.Rotation += 0.1f;
        return sprite;
    }
}
