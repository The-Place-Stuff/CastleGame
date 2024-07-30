using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Campfire : Object
{

    public Campfire(string name, string path) : base(name, path)
    {
    }

    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        animationTree.AddAnimation("assets/animation/campfire", true);

        base.Load();
    }


}
