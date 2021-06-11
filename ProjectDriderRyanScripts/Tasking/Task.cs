using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

public class Task : NetworkBehaviour
{
    [HideInInspector]
    //Assigned to player
    public int PlayerID;

    //Assigned to group
    [HideInInspector]
    public bool GroupTask;
    [HideInInspector]
    public int GroupID;

    //General Details
    public string TaskTitle;
    public string TaskDesc;

    [HideInInspector]
    public bool Active;
    [HideInInspector]
    public bool Complete;

    [HideInInspector]
    // 1 = Easy, 2 = Medium, 3 = Hard, 0 = Undefined
    public int Complexity;

    [HideInInspector]
    // 0 = Everyone, 1 = Group, 2 = Individual
    public int Target;

    public List<Goal> Goals;

    public virtual void Start()
    {
        if (int.TryParse(gameObject.name.Split(new char[] { ' ' })[1], out int numValue))
        {
            PlayerID = numValue;
            if(GroupTask)
            {
                GroupID = GM.groupBase.GetGroupByID(PlayerID);
            }
            else
            {
                GroupID = 0;
            }
            
            Debug.Log($"PlayerID in {this.TaskTitle} is {PlayerID} with groupID {GroupID}");
        }
        else
        {
            Debug.Log("Unable to get playerID in Catch5Foxes");
        }
        
    }

	public void CheckGoals()
	{
		Complete = Goals.All(g => g.Completed);
        this.enabled = false;
	}
}
