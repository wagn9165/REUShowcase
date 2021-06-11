using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Spawner : NetworkBehaviour
{
    private const float MaxRayCastDist = 50f;
    public static Spawner spawner;
    public GameObject spawnBox;
    public Vector3 returnable;

    private void Awake()
    {
        if (spawner != null)
        {
            Destroy(this);
        }
        else
        {
            spawner = this;
        }

        //DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GrabRandomVectorInSpawnBox()
    {
        var borderRange = 5.0f;
        returnable = new Vector3
        {
            x = Random.Range(spawnBox.transform.position.x + borderRange - (spawnBox.transform.localScale.x / 2), spawnBox.transform.position.x - borderRange + (spawnBox.transform.localScale.x / 2)),
            y = spawnBox.transform.position.y + (spawnBox.transform.localScale.y / 2),
            z = Random.Range(spawnBox.transform.position.z + borderRange - (spawnBox.transform.localScale.z / 2), spawnBox.transform.position.z - borderRange + (spawnBox.transform.localScale.z / 2))
        };
        //Debug.Log($"x {spawnBox.transform.position.x}, local scale {spawnBox.transform.localScale.x}");
        //Debug.Log($"x {spawnBox.transform.position.z}, local scale {spawnBox.transform.localScale.z}");


        return returnable;
    }

    [Command(requiresAuthority = false)]
    public void SpawnCreatureAtLocation(int creatureID, Vector3 location)
    {
        //Grabs creature by index from animal list and spawns at location told
        var creature = Instantiate(GM.animalList.GetList()[creatureID], location, Quaternion.identity);

        //deer.transform.GetChild(3).transform.localPosition = deer.transform.position;
        NetworkServer.Spawn(creature);
    }

    [Command(requiresAuthority = false)]
    public void Spawn100(int creatureID)
    {
        Vector3 targetLocation;
        for (int i = 0; i < 100; i++)
        {
            if (Physics.Raycast(GrabRandomVectorInSpawnBox(), Vector3.down, out RaycastHit hit, MaxRayCastDist))
            {
                targetLocation = hit.point;

                GameObject creatureGO = GM.animalList.GetList()[creatureID];
                targetLocation.y += creatureGO.transform.localScale.y;
                var creature = Instantiate(creatureGO, targetLocation, Quaternion.identity);
                NetworkServer.Spawn(creature);
            }

        }
    }

    [Command(requiresAuthority = false)]
    public void SpawnByRegion()
    {
        Vector3 targetLocation = new Vector3();
        RaycastHit hit;
        for(int j = 0; j < GM.sandboxNumbers.GetList().Count; j++)
        {
            GameObject creatureGO = GM.animalList.GetList()[j];
            for (int i = 0; i < GM.sandboxNumbers.GetList()[j]; i++)
            {
                if (Physics.Raycast(GrabRandomVectorInSpawnBox(), Vector3.down, out hit, MaxRayCastDist))
                {
                    targetLocation = hit.point;

                    
                    targetLocation.y += creatureGO.transform.localScale.y;
                    var creature = Instantiate(creatureGO, targetLocation, Quaternion.identity);
                    NetworkServer.Spawn(creature);
                }

            }
        }
        
    }

    [Command(requiresAuthority = false)]
    public void SpawnUsingColourRandom()
    {
        Vector3 targetLocation = new Vector3();
        RaycastHit hit;
        for (int j = 0; j < GM.sandboxNumbers.GetList().Count; j++)
        {
            GameObject creatureGO = GM.animalList.GetList()[j];
            for (int i = 0; i < GM.sandboxNumbers.GetList()[j]; i++)
            {
                Vector3 rando = GrabRandomVectorInSpawnBox();
                var pointColour = GM.colourSpawningExperiments.GetColorAtPoint(rando);
                if (Physics.Raycast(rando, Vector3.down, out hit, MaxRayCastDist) && ((pointColour.g > 0.5f) && (pointColour.r < 0.5f)))
                {
                    targetLocation = hit.point;


                    targetLocation.y += creatureGO.transform.localScale.y;
                    var creature = Instantiate(creatureGO, targetLocation, Quaternion.identity);
                    NetworkServer.Spawn(creature);
                }
                else
                {
                    i--;

                }

            }
        }
    }

    [Command(requiresAuthority = false)]
    public void SpawnCreatureAtLocationColour(int creatureID, Vector3 location)
    {
        //Grabs creature by index from animal list and spawns at location told
        var creature = Instantiate(GM.animalList.GetList()[creatureID], location, Quaternion.identity);

        var pointColour = GM.colourSpawningExperiments.GetColorAtPoint(location);
        Debug.Log($"Colour at Location: {pointColour} {location}");
        //deer.transform.GetChild(3).transform.localPosition = deer.transform.position;
        NetworkServer.Spawn(creature);
    }

    [Command(requiresAuthority = false)]
    public void SpawnUsingColourGiven(Color colour)
    {
        Vector3 targetLocation = new Vector3();
        RaycastHit hit;
        List<Vector3> pointList = GM.colourSpawningExperiments.GetPointsByColour(colour);
        for (int j = 0; j < GM.sandboxNumbers.GetList().Count; j++)
        {
            GameObject creatureGO = GM.animalList.GetList()[j];
            for (int i = 0; i < GM.sandboxNumbers.GetList()[j]; i++)
            {
                
                Vector3 rando = pointList[Random.Range(0, pointList.Count)];
                if (Physics.Raycast(rando, Vector3.down, out hit, MaxRayCastDist))
                {
                    targetLocation = hit.point;


                    targetLocation.y += creatureGO.transform.localScale.y;
                    var creature = Instantiate(creatureGO, targetLocation, Quaternion.identity);
                    NetworkServer.Spawn(creature);
                }
                else
                {
                    i--;

                }

            }
        }
    }

    [Command(requiresAuthority = false)]
    public void SpawnUsingColourGivenByOne(Color colour, int colourIndex)
    {
        Vector3 targetLocation = new Vector3();
        RaycastHit hit;
        List<Vector3> pointList = GM.colourSpawningExperiments.GetPointsByColourByOne(colour, colourIndex);
        for (int j = 0; j < GM.sandboxNumbers.GetList().Count; j++)
        {
            GameObject creatureGO = GM.animalList.GetList()[j];
            for (int i = 0; i < GM.sandboxNumbers.GetList()[j]; i++)
            {
                
                Vector3 rando = pointList[Random.Range(0, pointList.Count)];
                if (Physics.Raycast(rando, Vector3.down, out hit, MaxRayCastDist))
                {
                    targetLocation = hit.point;


                    targetLocation.y += creatureGO.transform.localScale.y;
                    var creature = Instantiate(creatureGO, targetLocation, Quaternion.identity);
                    NetworkServer.Spawn(creature);
                }
                else
                {
                    i--;

                }

            }
        }
    }

    [Command(requiresAuthority = false)]
    public void SpawnUsingColourGivenByOneTolerance(Color colour, int colourIndex)
    {
        Vector3 targetLocation = new Vector3();
        RaycastHit hit;
        List<Vector3> pointList = GM.colourSpawningExperiments.GetPointsByColourByOneTolerance(colour, colourIndex);
        for (int j = 0; j < GM.sandboxNumbers.GetList().Count; j++)
        {
            GameObject creatureGO = GM.animalList.GetList()[j];
            for (int i = 0; i < GM.sandboxNumbers.GetList()[j]; i++)
            {
                
                Vector3 rando = pointList[Random.Range(0, pointList.Count)];
                if (Physics.Raycast(rando, Vector3.down, out hit, MaxRayCastDist))
                {
                    targetLocation = hit.point;


                    targetLocation.y += creatureGO.transform.localScale.y;
                    var creature = Instantiate(creatureGO, targetLocation, Quaternion.identity);
                    NetworkServer.Spawn(creature);
                }
                else
                {
                    i--;

                }

            }
        }
    }

    [Command(requiresAuthority = false)]
    public void SpawnUsingColourGivenBySeveralTolerance(Color colour, float rTolerance, float gTolerance, float bTolerance)
    {
        Vector3 targetLocation;

        List<Vector3> pointList = GM.colourSpawningExperiments.GetPointsByColourBySeveralTolerance(colour, rTolerance, gTolerance, bTolerance);
        for (int j = 0; j < GM.sandboxNumbers.GetList().Count; j++)
        {
            GameObject creatureGO = GM.animalList.GetList()[j];
            for (int i = 0; i < GM.sandboxNumbers.GetList()[j]; i++)
            {
                int temp = Random.Range(0, pointList.Count);
                Vector3 rando = pointList[temp];
                if (Physics.Raycast(rando, Vector3.down, out RaycastHit hit, MaxRayCastDist))
                {
                    targetLocation = hit.point;
                    //Debug.Log($"{temp} Spawn colour: {GM.colourSpawningExperiments.GetColorAtPoint(targetLocation)} point {targetLocation}");

                    targetLocation.y += creatureGO.transform.localScale.y;
                    var creature = Instantiate(creatureGO, targetLocation, Quaternion.identity);
                    NetworkServer.Spawn(creature);
                }
                else
                {
                    i--;

                }

            }
        }
    }

    [Command(requiresAuthority = false)]
    public void SpawnCreatureUsingColourGivenBySeveralTolerance(int creatureID, int number, Color colour, float rTolerance, float gTolerance, float bTolerance)
    {
        Vector3 targetLocation;
        List<Vector3> pointList = GM.colourSpawningExperiments.GetPointsByColourBySeveralTolerance(colour, rTolerance, gTolerance, bTolerance);

        GameObject creatureGO = GM.animalList.GetList()[creatureID];
        for (int i = 0; i < number; i++)
        {

            Vector3 rando = pointList[Random.Range(0, pointList.Count)];
            if (Physics.Raycast(rando, Vector3.down, out RaycastHit hit, MaxRayCastDist))
            {

                targetLocation = hit.point;



                targetLocation.y += creatureGO.transform.localScale.y;
                var creature = Instantiate(creatureGO, targetLocation, Quaternion.identity);
                NetworkServer.Spawn(creature);
            }
            else
            {
                i--;

            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, .33f);
        if(spawnBox)
        {
            Gizmos.DrawCube(spawnBox.transform.position, spawnBox.transform.localScale);
        }
        
        Gizmos.DrawLine(returnable, returnable - new Vector3(0, 5, 0));
    }
}
