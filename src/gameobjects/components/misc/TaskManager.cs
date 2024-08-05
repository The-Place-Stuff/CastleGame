﻿using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class TaskManager : Component
    {
        public Task CurrentTask { get; private set; }
        public List<Task> Tasks { get; private set; } = new List<Task>();

        public TaskManager() : base(false)
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
                if (t.Name == task.Name)
                {
                    CurrentTask = task;
                    return;
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