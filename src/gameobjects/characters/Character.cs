using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public abstract class Character : GameObject
{
    public int Range { get; set; }

    public GameObject Target { get; set; } = GameObject.Empty();

    public float Speed { get; set; }

    public Vector2 CurrentDirection { get; set; }

    public float MaxHealth { get; set; }

    public Character(string name, float maxHealth, float speed, int range)
    {
        Name = name;
        MaxHealth = maxHealth;
        Speed = speed;
        Range = range;
    }

    public override void Load()
    {
        Layer = 3;

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
        Direction direction = CreateAndAddComponent<Direction>();
        MovementAI movementAI = CreateAndAddComponent<MovementAI>();
        TaskManager taskManager = CreateAndAddComponent<TaskManager>();
        Health health = new Health(MaxHealth); AddComponent(health);

        direction.Set(Direction.East().Name);

        stateMachine.AddState(CharacterStates.Wandering);
        stateMachine.AddState(CharacterStates.Mining);
        stateMachine.AddState(CharacterStates.Using);
        stateMachine.AddState(CharacterStates.Chopping);
        stateMachine.AddState(CharacterStates.Fighting);
        stateMachine.AddState(CharacterStates.Picking);
        stateMachine.AddState(CharacterStates.Adding);
        stateMachine.AddState(CharacterStates.Taking);


        stateMachine.SetState(CharacterStates.Wandering.Name);

        Random rnd = new Random();
        movementAI.Path = new Vector2(rnd.Next((int)Position.X - Range, (int)Position.X + Range), rnd.Next((int)Position.Y - Range, (int)Position.Y + Range));


        animationTree.AddAnimation("assets/animation/" + Name + "_idle", _ => direction.Name == Direction.None().Name);
        animationTree.AddAnimation("assets/animation/" + Name + "_east", _ => direction.Name == Direction.East().Name);
        animationTree.AddAnimation("assets/animation/" + Name + "_west", _ => direction.Name == Direction.West().Name);

        base.Load();
    }


    public override void Update()
    {
        UpdateDirection();

        base.Update();
    }

    public virtual void OnDestinationArrived()
    {
        MovementAI movementAI = GetComponent<MovementAI>();
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        Random rnd = new Random();

        if (Target.Name == "")
        {
           movementAI.Path = VectorHelper.Snap(new Vector2(rnd.Next((int)Position.X - Range, (int)Position.X + Range), rnd.Next((int)Position.Y - Range, (int)Position.Y + Range)), map.objectGrid.TileSize.X);
        }

        if (GetTasks().Count > 0)
        {
            CompleteTask();
        }

    }

    public List<Task> GetTasks()
    {
        TaskManager taskManager = GetComponent<TaskManager>();

        if (taskManager.Tasks.Count > 0)
        {
            return taskManager.Tasks;
        }
        return new List<Task>();
    }

    public Task GetCurrentTask()
    {
        TaskManager taskManager = GetComponent<TaskManager>();

        if (taskManager.CurrentTask != null)
        {
            return taskManager.CurrentTask;
        }

        return new Task(GameObject.Empty());
    }

    public virtual void AddTask(Task task)
    {
        DebugGui.Log(task.Target.Position+" " + GetTasks().Count);
        if (Player.BuildingMode) return;

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        TaskManager taskManager = GetComponent<TaskManager>();
        Vector2 position = task.Target.Position;

        GameObject gameObject = SceneManager.CurrentScene.GetGameObjectAt(VectorHelper.Snap(position, map.objectGrid.TileSize.X));
        GameObject obj = map.objectGrid.GetTileFromWorldCoordinates(VectorHelper.Snap(position, map.objectGrid.TileSize.X));

        GameObject taskTarget = GameObject.Empty();
        taskTarget.Position = position;

        if (gameObject != null) taskTarget = gameObject;
        if (obj != null) taskTarget = obj;

        if (taskTarget is Player) return;

        task.Target = taskTarget;
        taskManager.AddTask(task);

        if (GetTasks().Count == 1)
        {
            taskManager.SetTask(task);
            SetTarget(taskTarget);
            UpdateTasks();
        }
    }

    public virtual Task GetTaskTypeFromGameObject(GameObject target)
    {
        return new Task(GameObject.Empty());
    }

    public void CompleteTask() 
    {
        TaskManager taskManager = GetComponent<TaskManager>();

        taskManager.Tasks.RemoveAt(0);

        if (GetTasks().Count > 0)
        {
            taskManager.SetTask(GetTasks()[0]);
            UpdateTasks();
            SetTarget(GetCurrentTask().Target);
        }
    }

    public virtual void SetTarget(GameObject target)
    {
        Target = target;
    }

    public virtual void UpdateTasks()
    {
        GetCurrentTask().Start();

    }

    public virtual void UpdateDirection()
    {
        Direction direction = GetComponent<Direction>();

        if (CurrentDirection.X > 0)
        {
            direction.Set(Direction.East().Name);
        }
        if (CurrentDirection.X < 0)
        {
            direction.Set(Direction.West().Name);
        }

    }
}

