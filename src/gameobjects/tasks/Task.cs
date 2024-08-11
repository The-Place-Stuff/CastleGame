using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Task
    {
        public string Name { get; private set; } = "none";

        public string Type { get; private set; } = "none";

        public GameObject Target { get; private set; } = GameObject.Empty();

        public TaskManager TaskMachine { get; private set; }

        public Task(string type, GameObject obj)
        {
            if (obj == null)
            {

                obj = GameObject.Empty();
                Name = TaskTypes.None;
                Type = TaskTypes.None;
                Target = obj;


            }
            else
            {
                Name = type + obj.Name;
                Target = obj;
                Type = type;
            }
        }

        public Task(string type, Vector2 position)
        {
            Type = type;
            Name = type + position.ToString();
            Target = Tile.Empty();
            Target.Position = position;
        }





        public virtual void Initialize()
        {
            TaskMachine = Target.GetComponent<TaskManager>();
        }


        public virtual void Update()
        {
        }

        public virtual void Action()
        {

        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}
