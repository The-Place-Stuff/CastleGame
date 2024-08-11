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
        public GameObject Target { get; set; } = GameObject.Empty();
        public Character Character { get; private set; } 


        public TaskManager TaskManager { get; private set; }

        public Task(GameObject obj)
        {
            if (obj == null)
            {

                obj = GameObject.Empty();
                Name = TaskTypes.None;
                Target = obj;


            }
            else
            {
                Name = Name + obj.Name;
                Target = obj;
            }
        }

        public Task(Vector2 position)
        {
            Name = Name + position.ToString();
            Target = GameObject.Empty();
            Target.Position = position;
        }

        public void SetCharacter(Character character)
        {
            Character = character;
        }

        public virtual void Initialize()
        {
            TaskManager = Target.GetComponent<TaskManager>();
        }

        public virtual void Update()
        {
        }

        public virtual void Start()
        {

        }


        public virtual void Finish()
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
