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

    public Campfire(string name, ObjectProperties objectProperties) : base(name, objectProperties)
    {
    }

    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        animationTree.AddAnimation("assets/animation/campfire", _ =>  true);

        base.Load();
    }

    public override void Update()
    {
        base.Update();
    }


}
