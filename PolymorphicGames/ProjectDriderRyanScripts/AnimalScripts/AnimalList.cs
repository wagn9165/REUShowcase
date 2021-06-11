using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalList : MonoBehaviour
{
    List<GameObject> animalList;

    //Indexed prefabs of animals starting at index 0
    public GameObject fox;
    public GameObject bunny;
    public GameObject deer;
    public GameObject opossum;

    private void Awake()
    {
        animalList = new List<GameObject>()
        {
            fox,
            bunny,
            deer,
            opossum
        };
    }

    public List<GameObject> GetList()
    {
        return animalList;
    }
}
