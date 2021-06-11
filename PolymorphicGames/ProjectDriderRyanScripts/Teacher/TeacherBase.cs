using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<PlayerBase>().PlayerID != 0)
        {
            gameObject.GetComponent<TeacherBase>().enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PlayerBase>().PlayerID != 0)
        {
            return;
        }
    }

    public void ChangeStudentGroups()
    {
        //GM.groupBase.students;

        foreach(GameObject g in GM.groupBase.GetStudents())
        {
            Debug.Log($"{g}");
        }
    }
}
