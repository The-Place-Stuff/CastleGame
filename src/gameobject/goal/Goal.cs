using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.CodeDom;
using System.Collections.Generic;

namespace Tira;
public abstract class Goal
{
    public int Priority { get; set; }

    public Character Character { get; private set; }

    public GameObject Target { get; set; }

    protected GoalManager GoalManager { get; private set; }

    private List<Action> startSubscribers = new List<Action>();
    private List<Action> finishSubscribers = new List<Action>();
    private List<Action> failureSubscribers = new List<Action>();

    public Goal(Vector2 targetPosition, int priority)
    {
        Vector2 bitPosition = BitGrid.ConvertWorldCoordinatesToGridCoordinates(VectorHelper.Snap(targetPosition, 16));

        Bit bit = BitGrid.GetBit(bitPosition);

        if (bit != null && bit is Interactable) {
            Target = bit;
        }

        if (Target == null)
        {
            Target = GameObject.Empty();
            Target.Position = targetPosition;
        }

        Priority = priority;
    }

    public void Assign(Character character)
    {
        Character = character;
        GoalManager = character.GetComponent<GoalManager>();
    }

    public abstract void Update();

    public void OnStart(Action subscriber)
    {
        startSubscribers.Add(subscriber);
    }

    public virtual void Start()
    {
        foreach (Action subscriber in startSubscribers)
        {
            subscriber();
        }
    }

    public void OnFinish(Action subscriber)
    {
        finishSubscribers.Add(subscriber);
    }

    public virtual void Finish()
    {
        foreach (Action subscriber in finishSubscribers)
        {
            subscriber();
        }
    }

    public void OnFailure(Action subscriber)
    {
        failureSubscribers.Add(subscriber);
    }

    public virtual void Fail()
    {
        foreach (Action subscriber in failureSubscribers)
        {
            subscriber();
        }
    }
}
