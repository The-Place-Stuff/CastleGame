using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira
{
    public class Transformation
    {
        public bool IsTransforming { get; set; } = false;
        public double Time { get; set; } = 0;
        public Transformation()
        {
        }

        public virtual Sprite Transform(Sprite sprite) { return sprite; }

        public virtual Sprite Reset(Sprite sprite) { return sprite; }
    }
}
