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
    public Dictionary<Task, int> Tasks { get; private set; } = new Dictionary<Task, int>();

    public TaskManager() : base(false)
    {
    }

    public void AddTask(Task task, int priority = 0)
    {
        if (task == null) return;

        Player player = SceneManager.CurrentScene.GetGameObject<Player>();
        StateMachine playerStateMachine = player.GetComponent<StateMachine>();

       // if (playerStateMachine.CurrentState is BuildState) return;

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

        Tasks.Add(task, priority);

        task.SetCharacter(GameObject as Character);
        task.Initialize();

        if (Tasks.Count == 1) SetTask(task);
        
    }

    public void CompleteTask()
    {
        Tasks.Remove(CurrentTask);

        //Debug.WriteLine(Tasks.Count);

        if (Tasks.Count > 0)
        {
            SetTask(GetHighestPriorityTask());

            return;
        }

        StateMachine characterStateMachine = GameObject.GetComponent<StateMachine>();

        if (characterStateMachine.CurrentState.Name != "idle")
        {
            characterStateMachine.SetState("idle");
        }
    }


    public void SetTask(Task task)
    {
        if (CurrentTask != null)
        {
            CurrentTask.Exit();
        }

        foreach (Task t in Tasks.Keys)
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

        if (CurrentTask == null) return;

        CurrentTask.Update();
    }

    private Task GetHighestPriorityTask()
    {
        Task highestPriorityTask = null;

        foreach (Task task in Tasks.Keys.OrderByDescending(x => Tasks[x]))
        {
            if (task == CurrentTask) continue;

            highestPriorityTask = task;
            break;
        }

        return highestPriorityTask;
    }
}
