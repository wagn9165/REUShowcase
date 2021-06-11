using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBase : MonoBehaviour
{
    private int i = 0;
    private GameObject GetNearestFromList(List<GameObject> list, Vector3 point)
    {
        float nearestDistance = float.MaxValue;
        GameObject nearestObject = null;
        for (int i = 0; i < list.Count; i++)
        {
            float distance = Vector3.Distance(list[i].transform.position, point);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = list[i];
            }
        }
        return nearestObject;
    }

    #region "Students"
    public static List<GameObject> students = new List<GameObject>();

    public void AddStudent(GameObject student)
    {
        student.GetComponent<PlayerBase>().PlayerID = GetStudentCount();
        student.GetComponent<PlayerBase>().fullName = student.GetComponent<PlayerBase>().fullName + GetStudentCount().ToString();

        students.Add(student);
    }
    public void AddStudent(GameObject student, int groupID)
    {
        
        students.Add(student);
    }

    public void Groupify()
    {
        foreach(GameObject student in students)
        {
            switch (GM.teacherSettings.selectionStyle)
            {
                case 1: //Student Selection
                    if (GM.teacherSettings.GroupSize > 1)
                    {
                        //Grabs groupID off kiddo
                        student.GetComponent<PlayerBase>().FetchGroup(student.GetComponent<Mirror.NetworkIdentity>().connectionToClient, student);
                    }
                    else
                    {
                        //If there's only one group, put them all in that group
                        student.GetComponent<PlayerBase>().GroupID = 1;
                    }
                    break;
                case 2: //Teacher Selection
                    //Only modify kids that have not been assigned to a group already
                    if (student.GetComponent<PlayerBase>().GroupID == 0)
                    {
                        student.GetComponent<PlayerBase>().SetGroupIDWrapper(student, 1);
                    }
                    break;
                default: //Auto-Assign
                    student.GetComponent<PlayerBase>().GroupID = (i % GM.teacherSettings.GroupSize) + 1;
                    break;
            }
        }
    }

    public void RemoveStudent(GameObject student)
    {
        students.Remove(student);
    }

    public int GetStudentCount()
    {
        return students.Count;
    }

    public GameObject GetNearestStudent(Vector3 point)
    {
        return GetNearestFromList(students, point);
    }

    public List<GameObject> GetStudents()
    {
        return students;
    }

    public int GetGroupByID(int id)
    {
        foreach(GameObject student in students)
        {
            if(id == student.GetComponent<PlayerBase>().PlayerID)
            {
                return student.GetComponent<PlayerBase>().GroupID;
            }
        }
        return 0;
    }

    public List<GameObject> GetStudentsByGroup(int groupID)
    {
        List<GameObject> returnable = new List<GameObject>();

        foreach(GameObject student in students)
        {
            if(student.GetComponent<PlayerBase>().GroupID == groupID)
            {
                returnable.Add(student);
            }
        }
        return returnable;
    }
    #endregion
}
