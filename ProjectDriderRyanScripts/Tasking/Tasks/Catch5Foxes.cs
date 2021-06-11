using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Catch5Foxes : Task
{
    public override void Start()
    {
        base.Start();
        
        Debug.Log("Catch5Foxes assigned.");
        TaskTitle = "Catch5Foxes";
        TaskDesc = "Catch five foxes";
        Complexity = 1;
        Target = 0;
        Goals = new List<Goal>
        {
            new CaptureGoal(this, 1, Target, "Capture five foxes goal desc", false, 0, 5)
        };

        Goals.ForEach(g => g.Init());
    }
}
