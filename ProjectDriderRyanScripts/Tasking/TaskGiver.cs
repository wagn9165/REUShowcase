using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Defunct for now, will update again later 05/20/21
public class TaskGiver : MonoBehaviour
{
    public Task task;


    public GameObject taskWindow;
    public Text titleText;
    public Text descText;

    public void OpenQuestWindow()
    {
        taskWindow.SetActive(true);
        titleText.text = task.TaskTitle;
        descText.text = task.TaskDesc;

    }

    private void Update()
    {
        if(Input.GetKeyDown("l"))
        {
            OpenQuestWindow();
        }
    }
}

/* Create Task Giver GO and put TaskGiver and Catch5Foxes in
 * Create panel and two texts for details */
