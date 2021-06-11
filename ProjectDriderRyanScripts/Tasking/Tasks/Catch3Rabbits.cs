using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch3Rabbits : Task
{
    public override void Start()
    {
        base.Start();

        Debug.Log("Catch3Rabbits assigned.");
        TaskTitle = "Catch3Rabbits";
        TaskDesc = "Catch three rabbits";
        Complexity = 1;
        Target = 1;
        Goals = new List<Goal>
        {
            new CaptureGoal(this, 1, 1, "Capture three rabbits goal desc", false, 0, 3)
        };

        Goals.ForEach(g => g.Init());
    }
}