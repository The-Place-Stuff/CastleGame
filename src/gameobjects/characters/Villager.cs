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
        public Villager(string name) : base(name)
        {
            Speed = 20;
            Range = 50;

        }

        public override void Update()
        {
            base.Update();

            if (Input.Mouse.RightClickRelease())
            {

                AddTask(TaskTypes.Go, SceneManager.CurrentScene.GetGameObject<Cursor>().Position);

                AddTask(GetTaskTypeFromGameObject(Target), Target);

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
    }
}
