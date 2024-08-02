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
        public string Name { get; private set; }
        public Vector2 Position { get; private set; }

        public GameObject GameObject { get; private set; }
        public Tile Object { get; private set; }

        public TaskMachine TaskMachine { get; private set; }

        public Task(Tile obj)
        {
            Name = obj.Name;
            Position = obj.Position;
            Object = obj;
        }

        public void SetGameObject(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public virtual void Initialize()
        {
            TaskMachine = GameObject.GetComponent<TaskMachine>();
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
