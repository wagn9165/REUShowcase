using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public Task Task { get; set; }
    public int GoalID { get; set; }
    public string goalDesc { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }
    public float percentageComplete { get; set; }

    public virtual void Init()
    {
        //default init
    }
    public void Evaluate()
    {
        percentageComplete = CurrentAmount / RequiredAmount;
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        Completed = true;
        this.Task.CheckGoals();
        Debug.Log(Task.TaskTitle + " marked as complete");
    }
}
