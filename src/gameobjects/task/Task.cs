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

        public Vector2 Position { get; private set; }

        public GameObject Target { get; private set; } = Tile.Empty();

        public TaskMachine TaskMachine { get; private set; }

        public Task(string type, GameObject obj)
        {
            if (obj == null)
            {

                obj = GameObject.Empty();
                Name = TaskTypes.None;
                Position = obj.Position;
                Target = obj;


            }
            else
            {
                Name = type + obj.Name;
                Position = obj.Position;
                Target = obj;
            }
        }

        public Task(string type, Vector2 position)
        {
            Type = type;
            Name = type + position.ToString();
            Position = position;
            Target = Tile.Empty();
        }

        public void SetTarget(GameObject gameObject)
        {
            Target = gameObject;
        }



        public virtual void Initialize()
        {
            TaskMachine = Target.GetComponent<TaskMachine>();
        }


        public virtual void Update()
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
