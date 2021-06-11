using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TestingSendToStudents : NetworkBehaviour
{
    /*
     * A Big ole testing script, assume it's all hotcode
     */

    public Task task;
    GameObject tasks;
    int localPlayerID;
    int currO = 0;
    // Update is called once per frame
    private void Start()
    {
        
    }

    void Update()
    {
        if(isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Sending Hello");
            Hola();
            GM.teacherSelection.SpawnMenu();
        }

        if(isServer && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Test");
        }

        if(isLocalPlayer && Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Groupify me bb");
            //GM.groupBase.Groupify();
            //var test = Spawner.spawner.GrabRandomVectorInSpawnBox();
            //Debug.Log($"var {test}");
            //Spawner.spawner.SpawnCreatureAtLocation(currO++ % GM.animalList.GetList().Count, Spawner.spawner.GrabRandomVectorInSpawnBox());
            //var deer = Instantiate(GM.animalList.GetList()[2], new Vector3(27, 22, 27), Quaternion.identity);
            Spawner.spawner.Spawn100(0);
            //deer.transform.GetChild(3).transform.localPosition = deer.transform.position;
            //NetworkServer.Spawn(deer);
        }

        if (isLocalPlayer && Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Bunny bois");
            //GM.groupBase.Groupify();
            //var test = Spawner.spawner.GrabRandomVectorInSpawnBox();
            //Debug.Log($"var {test}");
            //Spawner.spawner.SpawnCreatureAtLocation(currO++ % GM.animalList.GetList().Count, Spawner.spawner.GrabRandomVectorInSpawnBox());
            //var deer = Instantiate(GM.animalList.GetList()[2], new Vector3(27, 22, 27), Quaternion.identity);
            //Spawner.spawner.SpawnCreatureAtLocationColour(0, new Vector3(95, 0, 5));
            //Spawner.spawner.SpawnUsingColourGivenByOne(Color.red, 0);
            Spawner.spawner.SpawnUsingColourGivenBySeveralTolerance(new Color(1f, 1f, 0f, 1f), 1f, 1f, 0f);
            //new Color(0.3f, 0.5f, 0.66f, 1.0f)
            //deer.transform.GetChild(3).transform.localPosition = deer.transform.position;
            //NetworkServer.Spawn(deer);
        }

        if (isLocalPlayer && Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Bunny bois");
            //GM.groupBase.Groupify();
            //var test = Spawner.spawner.GrabRandomVectorInSpawnBox();
            //Debug.Log($"var {test}");
            //Spawner.spawner.SpawnCreatureAtLocation(currO++ % GM.animalList.GetList().Count, Spawner.spawner.GrabRandomVectorInSpawnBox());
            //var deer = Instantiate(GM.animalList.GetList()[2], new Vector3(27, 22, 27), Quaternion.identity);
            //Spawner.spawner.SpawnByRegion();
            //Spawner.spawner.SpawnCreatureUsingColourGivenBySeveralTolerance(3, 100, Color.red, 0.1f, 0f, 0f);
            //Debug.Log($"Group Count: {GM.groupBase.GetStudentCount()}");
            Debug.Log($"{gameObject.transform.position}");
            //deer.transform.GetChild(3).transform.localPosition = deer.transform.position;
            //NetworkServer.Spawn(deer);
        }

        if (isServer && Input.GetKeyDown(KeyCode.P))
        {
            
            ShutDown();
        }

        if(isLocalPlayer && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("KeyCode Q detected");
            TeacherSettingsWrapper.tsw.ToggleMovement();
        }
    }

    [Command(requiresAuthority = false)]
    public void SpawnCreatureAtLocation(int creature, Vector3 location)
    {
        //Grabs creature by index from animal list and spawns at location told
        var deer = Instantiate(GM.animalList.GetList()[creature], location, Quaternion.identity);

        //deer.transform.GetChild(3).transform.localPosition = deer.transform.position;
        NetworkServer.Spawn(deer);
    }

    [Command]
    void Hola()
    {
        Debug.Log("received hola from client");
    }
    
    //[Command]
    GameObject CreateTaskList()
    {
        //List<List<GameObject>> test = new List<List<GameObject>>();
        if (isLocalPlayer)
        {
            localPlayerID = gameObject.GetComponent<PlayerBase>().PlayerID;
            Debug.Log($"TestingSendToStudents.localPlayerID = {localPlayerID}");
            Debug.Log($"TestingSendToStudents.CreateTaskList making \"Player {localPlayerID} Tasks\"");
            var tempTasks = new GameObject("Player " + gameObject.GetComponent<PlayerBase>().PlayerID + " Tasks");
            return tempTasks;
        }
        return null;
        
    }

    [Command]
    void IncreaseGroupSize()
    {
        TeacherSettingsWrapper.tsw._ChangeGroupSize(5);
    }

    [ClientRpc]
    void ShutDown()
    {
        Debug.Log("Cut that out or I will shut you down");
        if (isLocalPlayer)
        {
            Debug.Log("TestingSendToStudents.Shutdown is localplayer");

            if(tasks == null)
            {
                tasks = CreateTaskList();
            }

            Debug.Log($"Checking ID: {localPlayerID} that returned groupID: {GM.groupBase.GetGroupByID(localPlayerID)}");

            if (GM.groupBase.GetGroupByID(localPlayerID) != 0)
            {
                //Since the structure isnt solidified here is some garbage!
                tasks.AddComponent(typeof(GroupCatch2Foxes));
                Invoke("addTasksToUI",.5f);
            }
            else
            {
                Debug.Log("Could not assign task as player has no groupID");
            }

            Debug.Log("After");

            /*if (TeacherSettingsWrapper.tsw)
            {
                Debug.Log("Calling my tsw.ChangeGroup");

                TeacherSettingsWrapper.tsw.ChangeGroupSize(5);
            }
            else
            {
                Debug.LogError("Testing was not valid in current context");
            }*/

            //GM.teacherSettings.allCanMove = false;
        }
        else
        {
            Debug.Log("TestingSendToStudents.Shutdown is not localplayer");
        }
        
    }
    void addTasksToUI() {
        FindObjectOfType<Canvas>().transform.Find("PanelBLUR").GetComponent<ObjectivesSideList>().AddNewTask(tasks.GetComponent<GroupCatch2Foxes>());
    }

}


/*components = tasks.GetComponents(typeof(Component));
foreach (Component component in components)
{
Debug.Log(component.ToString());
}*/

/*if (components[0].ToString() == "Player " + gameObject.GetComponent<PlayerBase>().PlayerID + " Tasks (UnityEngine.Transform)")
{
    Debug.Log("Component 0 matches text");
}*/
