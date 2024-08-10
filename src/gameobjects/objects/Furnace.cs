using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Furnace : MakerObject
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
