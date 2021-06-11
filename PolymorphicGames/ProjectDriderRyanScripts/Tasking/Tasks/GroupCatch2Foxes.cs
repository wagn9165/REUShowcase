using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupCatch2Foxes : Task
{
    public override void Start()
    {
        #region TaskArguments

        Debug.Log("GroupTask assigned");
        TaskTitle = "GroupCatch2Foxes";
        TaskDesc = "Catch foxes but group testing time";
        Complexity = 0;
        Target = 0; //Animal ID
        GroupTask = true;

        #endregion

        base.Start();


        Goals = new List<Goal>
        {
            new CaptureGoal(this, 1, Target, "Gather 2 Foxes", false, 0, 2)
        };

        Goals.ForEach(g => g.Init());
    }

}
