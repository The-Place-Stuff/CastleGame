using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CastleGame;
public class OnState : GameObjectState
{
    public OnState() : base("off")
    {
    }

    public override void Enter()
    {
        Debug.WriteLine("Entering off state..");
    }

    public override void Update()
    {

    }
}
