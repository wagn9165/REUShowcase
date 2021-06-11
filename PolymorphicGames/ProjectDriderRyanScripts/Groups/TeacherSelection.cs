using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeacherSelection : MonoBehaviour
{
    public GameObject wholeHUD;

    #region GroupSliderVariables

    public Slider groupSizeSlider;

    #endregion

    #region GroupMenuNamePlateVariables

    /* Grabs group of students from GM for local list
     * Allows for creation of list while more students
     * are joining. Refreshes list upon menu spawning.*/
    GroupBase group;
    List<GameObject> students;

    //For creating more groupContainers
    List<GameObject> containers;
    public string groupContainerName = "GroupContainer";
    public GameObject groupContainer;

    //NamePlate prefab
    public GameObject namePlate;

    //List to hold spawned nameplates
    private List<GameObject> namePlateList;

    //For the features of the nameplates
    private Button leftButton;
    public string leftButtonName = "leftButton";
    public Sprite UI_ArrowL_Grey;

    private Button rightButton;
    public string rightButtonName = "rightButton";
    public Sprite UI_ArrowR_Grey;

    private TMP_Text namePlateText;
    public string namePlateTextName = "namePlateText";

    int tempGroupSize;

    //Coroutine
    private IEnumerator waitCoroutine;
    
    #endregion

    private void Start()
    {
        //Grabs group reference for shorthand
        group = GM.groupBase;

        //Initialize lists
        namePlateList = new List<GameObject>();
        containers = new List<GameObject>();

        //Initialize slider
        groupSizeSlider.value = GM.teacherSettings.GroupSize;
        tempGroupSize = (int)groupSizeSlider.value;
        groupSizeSlider.onValueChanged.AddListener(delegate { SliderGroupSize(); });

        //Setup Coroutine
        waitCoroutine = WaitFrame();

        //Hide menu
        wholeHUD.SetActive(false);
    }

    //WaitFrame: waits a frame then respawns menu to allow Unity's OnDestroy to fire and do memory management
    private IEnumerator WaitFrame()
    {
        DestroyNamePlates();

        yield return null;

        SpawnNamePlates();

        //Coroutine reinitialize
        StopCoroutine(waitCoroutine);
        waitCoroutine = WaitFrame();
    }

    //WaitContainers: waits a frame then respawns menu with altered group size
    private IEnumerator WaitContainers()
    {
        yield return null;

        /*Add new students to the group, needed because this coroutine is implemented by SliderGroupSize() instead
         * of SpawnMenu() */
        GM.groupBase.Groupify();

        //Shift out of bound students
        ShiftOutOfBoundStudents();

        //Respawn menu
        SpawnContainers();
        SpawnNamePlates();

        //Coroutine reinitialize
        StopCoroutine(waitCoroutine);
        waitCoroutine = WaitFrame();
    }

    //SpawnMenu: public facing menu invocation function
    public void SpawnMenu()
    {
        //Add any new students to a group
        GM.groupBase.Groupify();

        //If the HUD is not on
        if (wholeHUD.activeSelf == false)
        {
            //HUD on
            wholeHUD.SetActive(true);

            //Spawn elements of group change screen
            SpawnContainers();
            SpawnNamePlates();
        }
        else
        {
            //Wait frame before respawning menu
            StartCoroutine(waitCoroutine);
        }

    }

    //DisableMenu: Turns off the HUD
    public void DisableMenu()
    {
        //Clear menu
        DestroyNamePlates();
        DestroyContainers();

        wholeHUD.SetActive(false);
    }

    #region GroupSliderFunctions

    //SliderGroupSize: listener for slider value change
    public void SliderGroupSize()
    {
        //Clear menu
        DestroyNamePlates();
        DestroyContainers();

        //Tell everyone to update group size
        TeacherSettingsWrapper.tsw.ChangeGroupSize((int) groupSizeSlider.value);
        
        //Spawn menu
        waitCoroutine = WaitContainers();
        StartCoroutine(waitCoroutine);
    }

    //ShiftOutOfBoundStudents: Takes anyone over group size limit and puts them back within range
    private void ShiftOutOfBoundStudents()
    {
        foreach(GameObject student in students)
        {
            if(student.GetComponent<PlayerBase>().GroupID > GM.teacherSettings.GroupSize)
            {
                //Debug.Log($"Moving {student.GetComponent<PlayerBase>().fullName} to group {GM.teacherSettings.GroupSize} from {student.GetComponent<PlayerBase>().GroupID}");
                student.GetComponent<PlayerBase>().GroupID = GM.teacherSettings.GroupSize;
            }
        }
    }

    #endregion

    #region GroupMenuNamePlateFunctions

    //SpawnNamePlates: Instantiates the name plates for the menu
    private void SpawnNamePlates()
    {
        //Debugging, should not fire anymore, left to check for memory leaks as we continue
        if (namePlateList.Count != 0)
        {
            Debug.LogError($"You didn't clear the namePlateList before recreating it. Memory leak time Count {namePlateList.Count}");
            namePlateList.Clear();
            Debug.LogError($"Count after clear {namePlateList.Count}");
        }
        //Certainly is unnecessary, but ensures cleanliness if mistakes were made
        namePlateList.Clear();
        

        int i = 0;

        //Grab local reference for clarity
        students = group.GetStudents();

        foreach (GameObject student in students)
        {
            //Instantiating NamePlate
            //First instantiation is clone of nameplate in prefab, after clone disable prefab
            if (namePlate.activeSelf)
            {
                namePlateList.Add(Instantiate(namePlate));
                namePlate.SetActive(false);
            }
            else
            {
                //Spawning from disabled prefab requires reenabling of clone
                namePlateList.Add(Instantiate(namePlate));
                namePlateList[i].SetActive(true);
            }
            //Grab name for name tag
            namePlateList[i].name = student.GetComponent<PlayerBase>().fullName;

            //Debug.Log($"Searching for " + groupContainerName + student.GetComponent<PlayerBase>().GroupID);
            //Debug.Log($"Setting parent to {(GameObject.Find(groupContainerName + student.GetComponent<PlayerBase>().GroupID)).transform.GetChild(1).GetChild(0).GetChild(0).name}");

            //Set Parent
            //Quick fix if students manage to wiggle through getting assigned a group
            if(student.GetComponent<PlayerBase>().GroupID == 0)
            {
                student.GetComponent<PlayerBase>().GroupID = 1;
            }
            namePlateList[i].transform.SetParent((GameObject.Find(groupContainerName + student.GetComponent<PlayerBase>().GroupID)).transform.GetChild(1).GetChild(0).GetChild(0));
            namePlateList[i].transform.localScale = Vector3.one;
            namePlateList[i].SetActive(true);

            //Set nameplate text to name of student
            namePlateText = namePlateList[i].transform.Find(namePlateTextName).gameObject.GetComponent<TMP_Text>();
            namePlateText.text = student.GetComponent<PlayerBase>().fullName;

            //Find buttons
            leftButton = namePlateList[i].transform.Find(leftButtonName).gameObject.GetComponent<Button>();
            rightButton = namePlateList[i].transform.Find(rightButtonName).gameObject.GetComponent<Button>();
            
            //Add listeners
            /*Need to do this redeclaration to get a local reference to the student as using student raw uses the
             * memory address of student even though student is a changing value */
            GameObject tempStudent = student;

            leftButton.onClick.AddListener(() => DecrementStudentGroup(tempStudent));
            rightButton.onClick.AddListener(() => IncrementStudentGroup(tempStudent));

            //Set button image
            //Right button
            if (student.GetComponent<PlayerBase>().GroupID + 1 > tempGroupSize)
            {
                rightButton.interactable = false;
            }

            //Left button
            if (student.GetComponent<PlayerBase>().GroupID - 1 < 1) //min group is 1
            {
                leftButton.interactable = false;
            }

            //Debug.Log($"{namePlateList[i].name} initialized");
            i++;
        }
    }

    //DestroyNamePlates: Destroys the nameplates
    public void DestroyNamePlates()
    {
        int i = 0;

        for (i = 0; i < namePlateList.Count; i++)
        {
            //Debug.Log($"Grabing buttons from index {i} from list count {students.Count}");
            //Grab Buttons
            leftButton = namePlateList[i].transform.Find(leftButtonName).gameObject.GetComponent<Button>();
            rightButton = namePlateList[i].transform.Find(rightButtonName).gameObject.GetComponent<Button>();

            //Remove Listeners
            //Debug.Log("removing listeners");

            //Leaves edge case where if student leaves, listeners cannot be removed. Should be cleaned by Unity's OnDestroy
            if(i < students.Count)
            {
                GameObject tempStudent = students[i];
                leftButton.onClick.RemoveListener(() => DecrementStudentGroup(tempStudent));
                rightButton.onClick.RemoveListener(() => IncrementStudentGroup(tempStudent));
            }
            
            //Set parent to null
            namePlateList[i].transform.SetParent(null);

            Destroy(namePlateList[i]);

            //Do not remove from list YET as this breaks the iteration of the for loop
            namePlateList[i] = null;
        }
        
        //Clear list after every nameplate dealt with
        namePlateList.Clear();
    }

    //SpawnContainers: Spawns in the containers to hold the nameplates
    private void SpawnContainers()
    {
        //tempvar in case groupSizeSlider.value changes at a mismatched time with creation and destruction
        tempGroupSize = (int)groupSizeSlider.value;

        for (int i = 2; i <= tempGroupSize; i++)
        {
            //Create container
            containers.Add(Instantiate(groupContainer));

            //Set name
            containers[i - 2].name = groupContainerName + i.ToString();
            //Debug.Log($"Instantiate groupContainerName: {containers[i - 2].name}");

            //Set parent
            //                                      wholeHud        .TeacherPanel       .ContainerMask
            containers[i - 2].transform.SetParent(wholeHUD.transform.GetChild(0).GetChild(0));

            //Local scale goes weird due to wholeHUD scale, reset to one
            containers[i - 2].transform.localScale = Vector3.one;
            
            //Put the containers in a neat line
            containers[i - 2].GetComponent<RectTransform>().anchoredPosition3D = groupContainer.GetComponent<RectTransform>().anchoredPosition3D + new Vector3(groupContainer.GetComponent<RectTransform>().rect.width * (i - 1), 0, 0);
            
            //Disable Blank Name Plate
            //GroupContainer*          .ScrollView    .ViewPort   .Content     .User
            containers[i - 2].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);


            /* Group Container Title Properties */
            //Change titlecard name
            containers[i - 2].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Group " + (i).ToString();

            //Change color
            if((i - 2) < GM.teacherSettings.groupColors.Count)
            {
                containers[i - 2].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().color = GM.teacherSettings.groupColors[i];
            }
            else
            {
                containers[i - 2].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().color = GM.teacherSettings.groupColors[0];
            }
            
        }
    }

    //DestroyContainers: Destroys the containers
    private void DestroyContainers()
    {
        for (int i = 0; i < containers.Count; i++)
        {
            //Debug.Log($"Container count {containers.Count}");
            //Debug.Log($"Destroy groupContainerName: {groupContainerName + (i + 2).ToString()} {i}");
            containers[i].transform.SetParent(null);
            Destroy(containers[i]);

            //Do not remove from list YET as breaks iteration loop
            containers[i] = null;
        }
        //Remove all from list
        containers.Clear();
    }

    //IncrementStudentGroup: Right button listener
    public void IncrementStudentGroup(GameObject student)
    {
        //Debug.Log("Right button");
        if (student.GetComponent<PlayerBase>().GroupID + 1 > tempGroupSize)
        {
            Debug.LogError("Cannot increment player group past group size");
            return;
        }
        student.GetComponent<PlayerBase>().IncrementGroupWrapper(student);
        SpawnMenu();
    }

    //DecrementStudentGroup: Left button listener
    public void DecrementStudentGroup(GameObject student)
    {
        //Debug.Log("Left Button");
        if (student.GetComponent<PlayerBase>().GroupID - 1 < 1) //min group is 1
        {
            Debug.LogError("Cannot decrement player group past 1");
            return;
        }
        student.GetComponent<PlayerBase>().DecrementGroupWrapper(student);
        //Debug.Log("Decrement destroy complete, spawn");
        SpawnMenu();
    }

    #endregion
}
