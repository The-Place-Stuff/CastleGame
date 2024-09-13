using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Player : GameObject
{
    public override void Load()
    {
        Inventory inventory = CreateAndAddComponent<Inventory>();

        StateMachine stateMachine = new StateMachine();

        BuildState buildState = new BuildState();
        InteractState interactState = new InteractState();

        AddComponent(stateMachine);

        stateMachine.AddState(buildState);
        stateMachine.AddState(interactState);

        stateMachine.SetState("interact");

        base.Load();
    }

    public override void Update()
    {
        Layer = 2;

        base.Update();
    }

}
