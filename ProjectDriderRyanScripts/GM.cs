using UnityEngine;

/************************************************
game manager class for finding all other managers
other managers can then be accesed through this,
reducing the number of finds used overall.
Can also hold variables. Ex:   GM.gm.someVariable
************************************************/

public class GM : MonoBehaviour {
	public static GM gm;
	public static TeacherSettings teacherSettings;
	public static RM_forest forest;
	public static GroupBase groupBase;
	public static RandomWaypoints randomWaypoints;
    public static PlayerSelectionDropDown playerSelectionDropDown;
    public static TeacherSelection teacherSelection;
    public static AnimalList animalList;
    public static SandboxNumbers sandboxNumbers;
    public static ColourSpawningExperiments colourSpawningExperiments;

	void Awake() {
		if (gm != null) GameObject.Destroy(gm);
		else gm = this;
		DontDestroyOnLoad(this);


		teacherSettings = FindObjectOfType<TeacherSettings>();
        forest = FindObjectOfType<RM_forest>();
        randomWaypoints = FindObjectOfType<RandomWaypoints>();
		groupBase = FindObjectOfType<GroupBase>();
        playerSelectionDropDown = FindObjectOfType<PlayerSelectionDropDown>();
        teacherSelection = FindObjectOfType<TeacherSelection>();
        animalList = FindObjectOfType<AnimalList>();
        sandboxNumbers = FindObjectOfType<SandboxNumbers>();
        colourSpawningExperiments = FindObjectOfType<ColourSpawningExperiments>();
    }
}