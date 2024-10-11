using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class InteractState : GameObjectState
{
    public Character Character;
    public InteractState() : base("interact")
    {
    }

    public override void Update()
    {
        
        if(Input.Mouse.LeftClick() && Character != null)
        {
            if (Character is Villager villager)
            {
                StateMachine stateMachine = villager.GetComponent<StateMachine>();

                stateMachine.SetState("working");

                villager.AddTaskFromWorld();
            }
        }

        base.Update();
    }
}
