using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Bush : Object, Interactable
{
    public Bush(string name, ObjectProperties objectProperties) : base(name, objectProperties)
    {

    }

    public override void Load()
    {
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
        GameObjectState off = new GameObjectState("bush");
        GameObjectState on = new GameObjectState("bush_berries");

        stateMachine.AddState(off);
        stateMachine.AddState(on);

        stateMachine.SetState(off.Name);

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();

        animationTree.AddAnimation(Objects.GetPath(Name, AssetTypes.Animation), _ => stateMachine.CurrentState.Name == off.Name);
        animationTree.AddAnimation(Objects.GetPath(Name, AssetTypes.Animation) + "_berries", _ => stateMachine.CurrentState.Name == on.Name);


        base.Load();
    }

    public override void RandomUpdate()
    {
        if(GetComponent<StateMachine>().CurrentState.Name != "bush_berries")
        {
            Grow();
        }

        base.RandomUpdate();
    }

    public void Grow()
    {
        GetComponent<StateMachine>().SetState("bush_berries");
    }

    public Task GetTaskType(Villager villager)
    {
        if (villager.Item.Name != Item.Empty().Name) return null;

        return new HarvestBerriesTask(Position);
    }




}
