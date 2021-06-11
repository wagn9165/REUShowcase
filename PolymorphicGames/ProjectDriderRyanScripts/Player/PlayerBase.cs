using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBase : NetworkBehaviour
{
    public string fullName;
    private string fName;
    private string lName;

    [SerializeField]
    private int _groupID;

    public int GroupID
    {
        get { return _groupID; }
        set { _groupID = value; }
    }

    [SerializeField]
    private int _playerID;

    public int PlayerID
    {
        get { return _playerID; }
        set { _playerID = value; }
    }



    // Start is called before the first frame update
    void Start()
    {
        
        if (fullName != "")
        {
            //Literal Clown Code to meet overloaded function demands
            string[] expanded = fullName.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            //Grabs first name and last name for use in game tags
            fName = expanded[0];
            lName = expanded[1];
        }
        else
        {
            fullName = "Peter Parker";
        }
        

        /*foreach (var s in expanded)
        {
            Debug.Log($"Sub: {s}");
        }
        Debug.Log($"First name: {fName}");
        Debug.Log($"Last name: {lName}");*/

        GM.groupBase.AddStudent(gameObject);
        GroupID = 0;
        Debug.Log($"Current number of students {GM.groupBase.GetStudentCount()}");
    }

    private void OnGroupChange()
    {

        return;
    }

    private void OnDestroy()
    {
        GM.groupBase.RemoveStudent(gameObject);
    }

    [TargetRpc]
    public void FetchGroup(NetworkConnection target, GameObject player)
    {
        GM.playerSelectionDropDown.SpawnMenu(player);
    }

    public void SetGroupIDWrapper(GameObject player, int val)
    {
        Debug.Log($"PlayerBase.SetGroupID setting my({PlayerID}) GroupID to {val}");
        GroupID = val;
        SetGroupIDCommand(player, val);
    }

    [Command(requiresAuthority = false)]
    public void SetGroupIDCommand(GameObject player, int val)
    {
        Debug.Log($"PlayerBase.SetGroupIDCommand setting my GroupID to {val}");
        player.GetComponent<PlayerBase>().SetGroupID(connectionToClient, player, val);
        player.GetComponent<PlayerBase>().GroupID = val;
    }

    [TargetRpc]
    public void SetGroupID(NetworkConnection target, GameObject player, int val)
    {
        GroupID = val;
    }


    #region Increment/DecrementWrappers
    public void IncrementGroupWrapper(GameObject player)
    {
        Debug.Log("IncrementGroupWrapper");
        player.GetComponent<PlayerBase>().IncrementGroupCommand(player);
    }
    public void DecrementGroupWrapper(GameObject player)
    {
        Debug.Log("DecrementGroupWrapper");
        player.GetComponent<PlayerBase>().DecrementGroupCommand(player);
    }

    [Command(requiresAuthority = false)]
    public void IncrementGroupCommand(GameObject player)
    {
        Debug.Log("IncrementGroupCommand");
        player.GetComponent<PlayerBase>().IncrementGroup(player.GetComponent<Mirror.NetworkIdentity>().connectionToClient, player);
        if(player.GetComponent<PlayerBase>().PlayerID != 0)
        {
            player.GetComponent<PlayerBase>().GroupID++;
        }
    }

    [Command(requiresAuthority = false)]
    public void DecrementGroupCommand(GameObject player)
    {
        Debug.Log("DecrementGroupCommand");
        player.GetComponent<PlayerBase>().DecrementGroup(player.GetComponent<Mirror.NetworkIdentity>().connectionToClient, player);
        if(player.GetComponent<PlayerBase>().PlayerID != 0)
        {
            player.GetComponent<PlayerBase>().GroupID--;
        }
    }

    [TargetRpc]
    public void IncrementGroup(NetworkConnection target, GameObject player)
    {
        Debug.Log("IncrementGroup");
        player.GetComponent<PlayerBase>().GroupID++;
    }

    [TargetRpc]
    public void DecrementGroup(NetworkConnection target, GameObject player)
    {
        Debug.Log("DecrementGroup");
        player.GetComponent<PlayerBase>().GroupID--;
    }
    #endregion
}
