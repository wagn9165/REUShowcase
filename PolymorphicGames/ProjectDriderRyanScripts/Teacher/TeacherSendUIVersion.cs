using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class TeacherSendUIVersion : NetworkBehaviour {
    public Task task;
    GameObject tasks;
    // Update is called once per frame
    private void Start() {
        createTaskList();
    }

    void Update() {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.X)) {
            Debug.Log("Sending Hello");
            Hola();
        }

        if (isServer && Input.GetKeyDown(KeyCode.P)) {
            Debug.Log("Yelling at client");
            ShutDown();
            //TeacherSettings.StopMovement();
        }
    }

    [Command]
    void Hola() {
        Debug.Log("received hola from client");
    }

    void createTaskList() {
        tasks = new GameObject("Player " + gameObject.GetComponent<PlayerBase>().PlayerID + " Tasks");
        
    }

    [ClientRpc]
    void ShutDown() {
        Debug.Log("Cut that out or I will shut you down");
        Component[] components = tasks.GetComponents(typeof(Component));
        /*foreach(Component component in components)
		{
			Debug.Log(component.ToString());
		}*/
        if (tasks != null) {
            /*tasks.AddComponent(typeof(Catch5Foxes));
            tasks.AddComponent(typeof(Catch3Rabbits));*/
            tasks.AddComponent(typeof(GroupCatch2Foxes));
            Invoke("addTasksToUI",.5f);
        }
        Debug.Log("After");
        /*components = tasks.GetComponents(typeof(Component));
        foreach (Component component in components)
        {
            Debug.Log(component.ToString());
        }*/

        if (components[0].ToString() == "Tasks (UnityEngine.Transform)") {
            Debug.Log("Component 0 matches text");
        }

        //GM.teacherSettings.allCanMove = false;
    }

    //Thus is the worst thing ever but Its without modifying previous infestructure
    void addTasksToUI() {
        FindObjectOfType<Canvas>().transform.GetChild(0).GetComponent<ObjectivesSideList>().AddNewTask(tasks.GetComponent<GroupCatch2Foxes>());
    }
}

