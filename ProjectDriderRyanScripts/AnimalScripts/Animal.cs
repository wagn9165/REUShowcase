using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, IAnimal
{
    #region Identifying Parameters
    //This is the *general* animal ID, aka, Foxes in whole have the ID of 0, Bunnies have 1

    public int AnimalID { get; set; }

    //This is the ID specific to that animal, as in, Fox #0, Fox #1, just between foxes and Bunny #0, Bunny #1, just for bunnies

    public int ClassifierID { get; set; }

    public string AnimalName { get; set; }

    //This is used to track the index of the given animal for the classifierID
    //This int can also track the amount of the animal in the given scene
    public static int ClassifierIndex { get; set; }

    //Person who captured animal
    public GameObject Catcher { get; set; }

    //Region
    public Color CaughtArea { get; set; }

    //Ticks
    public int NumTicks { get; set; }

    #endregion

    #region Animal Information Paramaters



    //This is the "specific" name of the animal. The animal name is fox, the representative name is Red Fox. The animal name is Bunny, the representative name is Eastern Cottontail
    public string representativeName { get; set; }

    public int age { get; set; }

    public int lifeSpan { get; set; }

    public float weight { get; set; }

    public float height { get; set; }

    public string homeLocation { get; set; }

    public string speciesName { get; set; }

    public string genusName { get; set; }

    public string familyName { get; set; }

    public string orderName { get; set; }

    public string className { get; set; }

    public string phylumName { get; set; }

    public string kingdomName { get; set; }

    public string animalDescription { get; set; }

    public string funFact { get; set; }

    #endregion Animal Information Paramaters

    #region BodyInformation

    public Rigidbody rigidbodyRef;

    #endregion

    public virtual bool Die()
    {
        Debug.Log("Animal killed");
        CaughtArea = GM.colourSpawningExperiments.GetColorAtPoint(transform.position);
        Debug.Log($"CaughtArea: {CaughtArea}");
        NumTicks = (int)generateRandomRange(0f, 5f);
        Debug.Log($"NumTicks: {NumTicks}");
        CaptureEvents.AnimalCaptured(this);
        gameObject.SetActive(false);
        return true;
    }

    #region Utility Functions
    public float generateRandomRange(float minValue, float maxValue)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
    #endregion Utility Functions
}
