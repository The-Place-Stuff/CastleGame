using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public Tool Tool { get; set; } = Tool.Empty();
        public Item CurrentItem { get; set; } = Item.Empty();
        public Villager(string name, float maxHealth, float speed, int range) : base(name, maxHealth, speed, range)
        {
        }

        public override void Update()
        {
            base.Update();
            CurrentItem.Position = new Vector2(Position.X, Position.Y - 14);


            if (Input.Mouse.RightClickRelease())
            {

                AddTask(TaskTypes.Go, Game.cursor.Position);

                AddTask(GetTaskTypeFromGameObject(Target), Target);

                Target = GameObject.Empty();

            }

            UpdateTool();
        }

        public void UpdateTool()
        {
            if (Tool.Name != Tool.Empty().Name)
            {
                Tool.Position = new Vector2(Position.X + CurrentDirection.X * 6, Position.Y - 7);
                Tool.Layer = Layer + 1;
                if (GetDirection().Name == Direction.East().Name)
                {
                    Tool.GetComponent<Sprite>().Effect = SpriteEffects.FlipHorizontally;

                }
                else
                {
                    Tool.GetComponent<Sprite>().Effect = SpriteEffects.None;

                }
                UpdateToolAnimations();
            }
        }

        public void UpdateToolAnimations()
        {
            if(Tool.Name != Tool.Empty().Name)
            {
                if(GetComponent<StateMachine>().CurrentState == CharacterStates.Chopping)
                {
                    Tool.GetComponent<Sprite>().Rotation += CurrentDirection.X / 10;

                }
                else if (GetComponent<StateMachine>().CurrentState == CharacterStates.Mining)
                {
                    Tool.GetComponent<Sprite>().Rotation += CurrentDirection.X / 10;

                }
                else
                {
                    Tool.GetComponent<Sprite>().Rotation = 0;
                }

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


        public virtual void SetTool(Item item)
        {
            if (item is Tool tool) {
                if (Tool.Name != Tool.Empty().Name)
                {
                    SceneManager.CurrentScene.Remove(Tool);
                }
                Tool = tool;
                SceneManager.CurrentScene.AddGameObject(Tool);
            }
        }

        public virtual bool IsHolding(Tool tool)
        {
            if(Tool.Name == tool.Name)
            {
                return true;
            }
            return false;
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
            if (target is MakerObject)
            {
                return TaskTypes.Use;
            }
            if (target is Stockpile && CurrentItem.Name == Item.Empty().Name)
            {
                return TaskTypes.Take;
            }
            if (target is Stockpile && CurrentItem.Name != Item.Empty().Name)
            {
                return TaskTypes.Add;
            }
            if (target is Item)
            {
                return TaskTypes.Pick;

            }
            return base.GetTaskTypeFromGameObject(target);
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
                CurrentItem = item;
                SceneManager.CurrentScene.GetGameObject<Player>().GetComponent<Inventory>().Add(item);
                OnDestinationArrived();
            }
        }

        public virtual void Add(GameObject gameObject)
        {
            if (gameObject is Stockpile stockpile)
            {
                if (CurrentItem.Name != Item.Empty().Name && (CurrentItem.Name == stockpile.CurrentType || stockpile.CurrentType == Item.Empty().Name))
                {
                    stockpile.AddItem(CurrentItem);
                    CurrentItem = Item.Empty();
                }

                OnDestinationArrived();
            }
        }
        public virtual void Take(GameObject gameObject)
        {
            if (gameObject is Stockpile stockpile)
            {
                if (CurrentItem.Name == Item.Empty().Name && stockpile.Size > 0)
                {
                    CurrentItem = stockpile.GetInventory().GetLast();
                    stockpile.RemoveItem(stockpile.GetInventory().GetLast());
                }

                OnDestinationArrived();
            }
        }



    }
}
