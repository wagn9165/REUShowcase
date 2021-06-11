using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeacherSettingsWrapper : NetworkBehaviour
{
    public static TeacherSettingsWrapper tsw;

    private void Awake()
    {
        tsw = this;
        //DontDestroyOnLoad(this);
    }

    [Command(requiresAuthority = false)]
    public void ToggleMovement()
    {
        if (isServer)
        {
            Debug.Log("I am the server calling  the ClientRpc _toggleMovement()");
            _toggleMovement();
        }
        else
        {
            Debug.Log("I was not the server");
            return;
        }
    }

    [ClientRpc]
    private void _toggleMovement()
    {
        Debug.Log($"Calling _toggleMovement, currently: {GM.teacherSettings.allCanMove}");
        if(isClient)
        {
            if (GM.teacherSettings.allCanMove)
            {
                Debug.Log("Disabling Movement");
                GM.teacherSettings.allCanMove = false;
            }
            else
            {
                Debug.Log("Enabling Movement");
                GM.teacherSettings.allCanMove = true;
            }
        }
        else
        {
            Debug.Log("Nonlocal player attempting to reset allcanmove");
        }
        
        
    }

    [Command(requiresAuthority = false)]
    public void ChangeGroupSize(int val)
    {
        if(isServer)
        {
            Debug.Log("I am the server calling a client rpc");
            _ChangeGroupSize(val);
        }
        else
        {
            Debug.Log("I was not the server");
            return;
        }
    }

    [ClientRpc]
    public void _ChangeGroupSize(int val)
    {
        //One of these should not be working
        //Right now it appears isLocalPlayer is false which is confusing
        if (isClient)
        {
            //Debug.Log("LocalPlayerCalling _ChangeGroupSize");
            GM.teacherSettings.GroupSize = val;
        }
        else
        {
            Debug.Log("Nonlocal player attempting to change group size");
        }
    }
}
