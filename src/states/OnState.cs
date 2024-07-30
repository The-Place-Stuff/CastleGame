using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CastleGame;
public class OffState : GameObjectState
{
    public OffState() : base("on")
    {
    }

    public override void Enter()
    {
        Debug.WriteLine("Entering on state..");
    }

    public override void Update()
    {

    }
}
