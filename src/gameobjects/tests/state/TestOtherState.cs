using Microsoft.Xna.Framework.Input;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class TestOtherState : GameObjectState
{
    public TestOtherState() : base("other")
    {
    }

    public override void Enter()
    {
        Debug.WriteLine("Entering other state..");
    }

    public override void Update()
    {
        if (Input.Keyboard.GetKeyPress(Keys.E.ToString()))
        {
            Debug.WriteLine("Switching to default..");

            StateMachine.SetState("default");
        }
    }
}
