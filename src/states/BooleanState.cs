using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CastleGame;
public class BooleanState : GameObjectState
{
    public BooleanState(string name) : base(name)
    {
    }

    public override void Enter()
    {
        Debug.WriteLine("Entering boolean state..");
    }

    public override void Update()
    {

    }
}
