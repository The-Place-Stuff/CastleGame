using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Campfire : Landmark
{

    public Campfire(string name, int radius, ObjectProperties objectProperties) : base(name, radius, objectProperties)
    {
    }

    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        animationTree.AddAnimation(Objects.GetPath(Name, AssetTypes.Animation), _ =>  true);

        base.Load();
    }

    public override void Update()
    {
        base.Update();
    }


}
