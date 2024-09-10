using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class TaskManager : Component
{
    public Task CurrentTask { get; private set; }
    public List<Task> Tasks { get; private set; } = new List<Task>();

    public TaskManager() : base(false)
    {
    }

    public void AddTask(Task task)
    {
        if (task == null) return;

        if (Player.BuildingMode) return;

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        Vector2 position = task.Target.Position;

        GameObject gameObject = SceneManager.CurrentScene.GetGameObjectAt<Character>(VectorHelper.Snap(position, map.objectGrid.TileSize.X));
        GameObject obj = SceneManager.CurrentScene.GetGameObjectAt<Item>(VectorHelper.Snap(position, map.objectGrid.TileSize.X));
        if(obj == null)
        {
            obj = map.objectGrid.GetTileFromWorldCoordinates(VectorHelper.Snap(position, map.objectGrid.TileSize.X));
        }

        GameObject taskTarget = GameObject.Empty();
        taskTarget.Position = position;

        if (gameObject != null) taskTarget = gameObject;
        if (obj != null) taskTarget = obj;

        if (taskTarget is Player) return;

        task.Target = taskTarget;
        //DebugGui.Log(taskTarget.Name + " is the task target");

        Tasks.Add(task);
        task.SetCharacter(GameObject as Character);
        task.Initialize();

        if (Tasks.Count == 1) SetTask(task);
        
    }

    public void CompleteTask()
    {
        Tasks.Remove(CurrentTask);

        //Debug.WriteLine(Tasks.Count);

        if (Tasks.Count > 0) SetTask(Tasks[0]);
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
                break;
            }

        }

        CurrentTask.Enter();

        CurrentTask.Start();
    }

    public override void Update()
    {
        if(Tasks.Count == 0)
        {
            CurrentTask = null;
            return;
        }

        CurrentTask.Update();
    }
}
