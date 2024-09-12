using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Transformation
    {

        public Transformation()
        {
        }

        public virtual Sprite Transform(Sprite sprite) { return sprite; }
    }
}
