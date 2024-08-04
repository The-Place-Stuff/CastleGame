using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Furnace : Object
{
    public Furnace(string name) : base(name)
    {
    }

    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();


        stateMachine.AddState(States.Off);
        stateMachine.AddState(States.On);

        GetComponent<StateMachine>().SetState("off");

        animationTree.AddAnimation("assets/animation/furnace_off", _ => GetComponent<StateMachine>().CurrentState.Name == "off");
        animationTree.AddAnimation("assets/animation/furnace_on", _ => GetComponent<StateMachine>().CurrentState.Name == "on");

        base.Load();
    }

    public override void Update()
    {


        base.Update();
    }

    public override void OnUse()
    {

        if (GetComponent<StateMachine>().CurrentState.Name == States.Off.Name)
        {
            GetComponent<StateMachine>().SetState(States.On.Name);
        }
        else
        {
            GetComponent<StateMachine>().SetState(States.Off.Name);

        }
        base.OnUse();
    }


}
