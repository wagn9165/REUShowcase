using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxNumbers : MonoBehaviour
{
    public int numDeer = 3;
    List<int> numCreature;

    private void Awake()
    {
        numCreature = new List<int>
        {
            5,
            5,
            3,
            100
        };
    }

    public List<int> GetList()
    {
        return numCreature;
    }

   
}
