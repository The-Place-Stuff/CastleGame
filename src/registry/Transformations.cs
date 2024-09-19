using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Transformations
    {
        public static readonly Transformation ToolHit = new ToolHitTransformation();
        public static readonly Transformation ObjectHit = new ObjectHitTransformation();
    }
}
