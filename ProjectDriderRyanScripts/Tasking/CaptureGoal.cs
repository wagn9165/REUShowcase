using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureGoal : Goal
{
    public int AnimalID { get; set; }

    public CaptureGoal(Task task, int goalID, int animalID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Task = task;
        this.GoalID = goalID;
        this.AnimalID = animalID;
        this.goalDesc = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init(); //Currently empty
        CaptureEvents.OnAnimalCapture += AnimalCaptured;
    
    }

    void AnimalCaptured(IAnimal animal)
    {
        Debug.Log("Heard any capture");
        if ((animal.AnimalID == this.AnimalID) && ((animal.Catcher.GetComponent<PlayerBase>().PlayerID == this.Task.PlayerID) || (animal.Catcher.GetComponent<PlayerBase>().GroupID == this.Task.GroupID)))
        {
            Debug.Log("Detected animal capture " + animal.AnimalName + " from " + animal.Catcher.GetComponent<PlayerBase>().PlayerID);
            this.CurrentAmount++;
            Evaluate();
        }
    }
}
