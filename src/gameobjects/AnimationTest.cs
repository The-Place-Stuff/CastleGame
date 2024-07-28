using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class AnimationTest : GameObject
{
    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();

        animationTree.AddAnimation("assets/animation/test", true);
    }
}