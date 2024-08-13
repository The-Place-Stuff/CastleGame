using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Furnace : Workstation
{
    public Furnace(string name) : base(name)
    {
    }

    public override void Load()
    {

        base.Load();
    }

    public override void Update()
    { 

        base.Update();
    }

    public override void OnUse()
    {
        StateMachine stateMachine = GetComponent<StateMachine>();

        if (stateMachine.CurrentState.Name == States.Off.Name)
        {
            stateMachine.SetState(States.On.Name);
            return;
        }

        stateMachine.SetState(States.Off.Name);
    }
}
