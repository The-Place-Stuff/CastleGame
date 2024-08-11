using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class MakerObject : Object
    {

        public int InventorySize { get; set; } = 0;

        public MakerObject(string name) : base(name)
        {


        }

        public override void Load()
        {
            Inventory inventory = new Inventory(InventorySize); AddComponent(inventory);
            AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
            StateMachine stateMachine = CreateAndAddComponent<StateMachine>();


            stateMachine.AddState(States.Off);
            stateMachine.AddState(States.On);

            GetComponent<StateMachine>().SetState("off");

            animationTree.AddAnimation("assets/animation/" + Name + "_off", _ => GetComponent<StateMachine>().CurrentState.Name == "off");
            animationTree.AddAnimation("assets/animation/" + Name + "_on", _ => GetComponent<StateMachine>().CurrentState.Name == "on");
            base.Load();
        }

        public void AddItem(Item item)
        {
            GetInventory().Add(item);
        }

        public void RemoveLastItem()
        {
            GetInventory().RemoveLast();
        }

        public void RemoveItem(Item item)
        {
            GetInventory().Remove(item);
        }

        public Inventory GetInventory()
        {
            return GetComponent<Inventory>();
        }
    }
}
