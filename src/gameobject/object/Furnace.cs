using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Furnace : Workstation
{
    public Furnace(string name, ObjectProperties objectProperties) : base(name, objectProperties)
    {
    }

    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        animationTree.AddAnimation("assets/animation/objects/furnace", _=> true);
        base.Load();
    }
}
