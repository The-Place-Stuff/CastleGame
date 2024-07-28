using Microsoft.Xna.Framework.Input;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class TestDefaultState : GameObjectState
{
    public TestDefaultState() : base("default")
    {
    }

    public override void Enter()
    {
        Debug.WriteLine("Entering default state..");
    }

    public override void Update()
    {
        if (Input.Keyboard.GetKeyPress(Keys.Q.ToString()))
        {
            Debug.WriteLine("Switching to other..");

            StateMachine.SetState("other");
        }
    }
}
