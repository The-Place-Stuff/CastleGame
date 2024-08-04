using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class TaskMachine : Component
    {
        public Task CurrentTask { get; private set; }
        public List<Task> Tasks { get; private set; } = new List<Task>();

        public TaskMachine() : base(false)
        {
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);

            task.SetTarget(GameObject);

            task.Initialize();
        }

        public void CompleteTask()
        {
            Tasks.Remove(CurrentTask);
        }


        public void SetTask(Task task)
        {
            if (CurrentTask != null)
            {
                CurrentTask.Exit();
            }

            foreach (Task t in Tasks)
            {
                Debug.WriteLine(t.Name);
                if (t.Name == task.Name)
                {
                    CurrentTask = task;
                }
                else
                {
                    Debug.WriteLine("Task " + "'" + task.Name + "' does not exist!");
                }
            }


            CurrentTask.Enter();
        }

        public override void Update()
        {
            if (CurrentTask != null)
            {
                CurrentTask.Update();
            }
        }
    }
}
