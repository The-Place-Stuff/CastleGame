using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class Bush : Bit
{
    public Bush(string name, BitProperties bitProperties) : base(name, bitProperties)
    {

    }

    public override void Load()
    {
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
        GameObjectState off = new GameObjectState("bush");
        GameObjectState on = new GameObjectState("bush_berries");

        stateMachine.AddState(off);
        stateMachine.AddState(on);

        stateMachine.SetState(off.Name);

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();

        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation), _ => stateMachine.CurrentState.Name == off.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_berries", _ => stateMachine.CurrentState.Name == on.Name);


        base.Load();
    }

    public void Grow()
    {
        GetComponent<StateMachine>().SetState("bush_berries");
    }

}
