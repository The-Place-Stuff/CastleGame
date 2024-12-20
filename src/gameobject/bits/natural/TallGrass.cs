using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class TallGrass : Bit
{
    public TallGrass(string name, BitProperties bitProperties) : base(name, bitProperties)
    {

    }
    public override void Load()
    {
        Sprite sprite = new Sprite(Bits.GetPath(Name, AssetTypes.Image));
        AddComponent(sprite);

        base.Load();
    }
}
