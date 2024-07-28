using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Test : GameObject
{
    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();

        animationTree.AddAnimation("assets/animation/test", true);

        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
        
        stateMachine.AddState(new TestDefaultState());
        stateMachine.AddState(new TestOtherState());

        stateMachine.SetState("default");
    }
}