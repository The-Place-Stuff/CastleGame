using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class Task
{
    public string Name { get; private set; } = "none";
    public GameObject Target { get; set; } = GameObject.Empty();
    public GameObject Sensor { get; set; } = GameObject.Empty();
    public Character Character { get; private set; } 

    private List<Action> finishSubscribers = new List<Action>();

    private List<Action> failureSubscribers = new List<Action>();

    public Task(GameObject bit)
    {
        if (bit == null)
        {

            bit = GameObject.Empty();
            Name = "none";
            Target = bit;
        }
        else
        {
            Target = bit;
        }
    }

    public Task(Vector2 position)
    {
        Target = GameObject.Empty();
        Target.Position = position;

    }

    public void SetCharacter(Character character)
    {
        Character = character;
    }

    public virtual void Initialize()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Start()
    {

    }


    public virtual void Finish()
    {
        foreach (Action action in finishSubscribers)
        {
            action.Invoke();
        }

    }

    public void OnFinish(Action action)
    {
        finishSubscribers.Add(action);
    }

    public virtual void Fail()
    {
        foreach (Action action in failureSubscribers)
        {
            action.Invoke();
        }

    }

    public void OnFailure(Action action)
    {
        failureSubscribers.Add(action);
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
}
