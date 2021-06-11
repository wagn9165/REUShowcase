using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelectionDropDown : MonoBehaviour
{
    public GameObject test;
    public Dropdown groupDropDown;

    public GameObject EmptyButtonTMP;
    public GameObject parentPanel;

    List<GameObject> buttonList = new List<GameObject>();

    private int valReturn;

    [SerializeField]
    private int _selectionStyle;

    public int SelectionStyle { get { return _selectionStyle; } set { _selectionStyle = value; } }

    public GameObject Player { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        test.SetActive(false);
        //Debug.Log($"Name of gameobject: {groupDropDown.transform.parent.parent.gameObject.ToString()}");
        //groupDropDown.gameObject.SetActive(false);
        //groupDropDown.enabled = false;
    }

    public void SpawnMenu(GameObject player)
    {
        Player = player;

        if (SelectionStyle == 0)
        {
            SpawnDropdown();
        }
        else if(SelectionStyle == 1)
        {
            SpawnBoxes();
        }
        else
        {
            throw new ArgumentException("PlayerSelectionDropDown.SelectionStyle is out of range");
        }
        
        
    }

    private void SpawnDropdown()
    {
        groupDropDown.ClearOptions();

        test.SetActive(true);

        List<string> groups = new List<string>();

        groups.Add("Select");

        for (int i = 1; i <= GM.teacherSettings.GroupSize; i++)
        {
            groups.Add(i.ToString());
        }

        groupDropDown.AddOptions(groups);

        groupDropDown.onValueChanged.AddListener(DropdownValueChanged);
        //Debug.Log($"Returning {valReturn}");
    }

    private void SpawnBoxes()
    {
        

        int DivSize = 400 / GM.teacherSettings.GroupSize;

        Button button;

        for (int i = 0; i < GM.teacherSettings.GroupSize; i++)
        {
            buttonList.Add(Instantiate(EmptyButtonTMP, new Vector3(200+(i*DivSize), 200), Quaternion.identity));
            buttonList[i].transform.SetParent(parentPanel.transform);
            
            button = buttonList[i].GetComponent<UnityEngine.UI.Button>();
            buttonList[i].GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();


            //Have to do switch because when casting as below
            //button.onClick.AddListener(() => ButtonOnClick(i));
            //Unity passes i's memory location leading to last i val always being called

            int tempInt = i + 1;

            button.onClick.AddListener(() => ButtonOnClick(tempInt));
            
            //This is how I tried it first
            /*switch(i)
            {
                case 0:
                    button.onClick.AddListener(() => ButtonOnClick(1));
                    break;
                case 1:
                    button.onClick.AddListener(() => ButtonOnClick(2));
                    break;
                case 2:
                    button.onClick.AddListener(() => ButtonOnClick(3));
                    break;
                case 3:
                    button.onClick.AddListener(() => ButtonOnClick(4));
                    break;
                case 4:
                    button.onClick.AddListener(() => ButtonOnClick(5));
                    break;
                default:
                    throw new ArgumentException("PlayerSelectionDropDown.SpawnBoxes.i is out of range");
            }*/
            
        }
        
        
    }
    
    private void DropdownValueChanged(int tests)
    {
        //Debug.Log($"New group value {groupDropDown.value}");
        Player.GetComponent<PlayerBase>().SetGroupIDWrapper(Player, groupDropDown.value);
        test.SetActive(false);
    }

    private void ButtonOnClick(int val)
    {
        Player.GetComponent<PlayerBase>().SetGroupIDWrapper(Player, val);
        foreach(GameObject g in buttonList)
        {
            g.SetActive(false);
        }
    }
   
}
