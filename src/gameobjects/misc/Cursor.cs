using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Cursor : GameObject
    {
        Sprite sprite;

        public Cursor()
        {
           sprite = new Sprite(Registry.Path + "cursor");
        }

        public override void Update()
        {
            base.Update();
        }



    }
}
