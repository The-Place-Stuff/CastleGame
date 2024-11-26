using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira
{
    public class Workbench : Workstation
    {
        public Workbench(string name, BitProperties bitProperties) : base(name, bitProperties)
        {
        }

        public override void Load()
        {
            Sprite sprite = new Sprite(Bits.GetPath(Name, AssetTypes.Image));
            AddComponent(sprite);
            base.Load();
        }
    }
}
