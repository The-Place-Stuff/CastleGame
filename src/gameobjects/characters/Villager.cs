using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Villager : Character
    {

        public Item currentItem = Item.Empty();
        public Villager(string name) : base(name)
        {
            Speed = 20;
            Range = 50;

        }

        public override void Update()
        {
            base.Update();
            currentItem.Position = new Vector2(Position.X, Position.Y - 14);
            if (Input.Mouse.RightClickRelease())
            {

                AddTask(TaskTypes.Go, Game.cursor.Position);

                AddTask(GetTaskTypeFromGameObject(Target), Target);

                Target = GameObject.Empty();

            }
        }

        public override void UpdateTasks()
        {
            base.UpdateTasks();
            if (GetCurrentTask().Type == TaskTypes.Use)
            {
                GetComponent<StateMachine>().SetState(CharacterStates.Using.Name);
                Use(GetComponent<TaskManager>().CurrentTask.Target);
            }
            else if (GetCurrentTask().Type == TaskTypes.Chop)
            {
                GetComponent<StateMachine>().SetState(CharacterStates.Chopping.Name);
                Chop(GetComponent<TaskManager>().CurrentTask.Target);
            }
            else if (GetCurrentTask().Type == TaskTypes.Mine)
            {
                GetComponent<StateMachine>().SetState(CharacterStates.Mining.Name);
                Mine(GetComponent<TaskManager>().CurrentTask.Target);
            }
            else if (GetCurrentTask().Type == TaskTypes.Pick)
            {
                GetComponent<StateMachine>().SetState(CharacterStates.Picking.Name);
                Pick(GetComponent<TaskManager>().CurrentTask.Target);
            }
            else if (GetCurrentTask().Type == TaskTypes.Add)
            {
                GetComponent<StateMachine>().SetState(CharacterStates.Adding.Name);
                Add(GetComponent<TaskManager>().CurrentTask.Target);
            }
            else if (GetCurrentTask().Type == TaskTypes.Take)
            {
                GetComponent<StateMachine>().SetState(CharacterStates.Taking.Name);
                Take(GetComponent<TaskManager>().CurrentTask.Target);
            }
        }

        //Task methods
        public virtual void Chop(GameObject gameObject)
        {
            if (gameObject is Tree tree)
            {
                tree.OnChop();
                OnDestinationArrived();
            }
        }

        public virtual void Mine(GameObject gameObject)
        {
            if (gameObject is Rock rock)
            {
                rock.OnMine();
                OnDestinationArrived();
            }
        }

        public virtual void Use(GameObject gameObject)
        {
            if (gameObject is Object obj)
            {
                obj.OnUse();
                OnDestinationArrived();
            }
        }

        public virtual void Pick(GameObject gameObject)
        {
            if (gameObject is Item item)
            {
                currentItem = item;
                SceneManager.CurrentScene.GetGameObject<Player>().GetComponent<Inventory>().Add(item);
                OnDestinationArrived();
            }
        }

        public virtual void Add(GameObject gameObject)
        {
            if (gameObject is Stockpile stockpile)
            {
                if (currentItem.Name != Item.Empty().Name && (currentItem.Name == stockpile.CurrentType || stockpile.CurrentType == Item.Empty().Name))
                {
                    stockpile.AddItem(currentItem);
                    currentItem = Item.Empty();
                }

                OnDestinationArrived();
            }
        }
        public virtual void Take(GameObject gameObject)
        {
            if (gameObject is Stockpile stockpile)
            {
                if (currentItem.Name == Item.Empty().Name && stockpile.Size > 0)
                {
                    currentItem = stockpile.GetInventory().GetLast();
                    stockpile.RemoveItem(stockpile.GetInventory().GetLast());
                }

                OnDestinationArrived();
            }
        }


        public override string GetTaskTypeFromGameObject(GameObject target)
        {
            if (target is Tree)
            {
                return TaskTypes.Chop;
            }
            if (target is Rock)
            {
                return TaskTypes.Mine;
            }
            if (target is Furnace)
            {
                return TaskTypes.Use;
            }
            if (target is Stockpile && currentItem.Name == Item.Empty().Name)
            {
                return TaskTypes.Take;
            }
            if (target is Stockpile && currentItem.Name != Item.Empty().Name)
            {
                return TaskTypes.Add;
            }
            if (target is Item)
            {
                return TaskTypes.Pick;

            }
            return base.GetTaskTypeFromGameObject(target);
        }

    }
}
