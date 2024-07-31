using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Bluprint : Object
    {
        public string currentBuilding;
        public Bluprint(string name) : base(name)
        {
            Sprite sprite = new Sprite(Objects.GetPath(name));

            AddComponent(sprite);
        }
    }
}
