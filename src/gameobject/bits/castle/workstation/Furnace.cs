using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class Furnace : Workstation
{
    public Furnace(string name, BitProperties bitProperties) : base(name, bitProperties)
    {
    }

    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation), _=> true);
        base.Load();
    }
}
