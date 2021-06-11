using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeacherSettings : NetworkBehaviour
{
    public bool allCanMove = true;

    //Groups
    [SerializeField]
    private int _groupSize = 1;
    public int GroupSize { get { return _groupSize; } set { _groupSize = value; } }

    public List<Color32> groupColors;
    
    public int selectionStyle;
        //Ideas:    0 - AutoAssign
        //          1 - Student Selection
        //          2 - Teacher Selection

    //Animals
    public bool ticks = true;
    public bool bunnies = true;
    public bool foxes = true;
    public bool moose = true;
    public bool deer = true;
    public bool birds = true;
    public bool lizards = true;

    //Features
    public int complexity = 0;
    public int timeLimit = 0;
    public bool tranquilizer = true;
    public bool birdTrapping = true;

    //Season
    public int season = 2;
        // 0 - Winter
        // 1 - Spring
        // 2 - Summer
        // 3 - Fall
    

    //Misc
    public bool skittishDeer = true;
    public bool meanMoose = true;
    public bool lizardHide = true;
    public bool studentNetBonk = true;
    public bool biomeLockAnimals = false;

    /*[ClientRpc]
    public void StopMovement()
    {
        GM.teacherSettings.allCanMove = false;
    }*/

    //Awake
    private void Awake()
    {
        groupColors = new List<Color32>()
        {
            new Color32(255,255,255,255),
            new Color32(20,212,20,255),
            new Color32(224,168,0,255),
            new Color32(255,99,99,255),
            new Color32(224,108,255,255)
        };
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
