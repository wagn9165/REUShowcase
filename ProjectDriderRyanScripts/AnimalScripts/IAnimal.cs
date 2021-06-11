using System.Collections;
using UnityEngine;

public interface IAnimal
{
    #region Identifying Parameters

    //This is the *general* animal ID, aka, Foxes in whole have the ID of 0, Bunnies have 1
    int AnimalID { get; set; }

    //This is the ID specific to that animal, as in, Fox #0, Fox #1, just between foxes and Bunny #0, Bunny #1, just for bunnies
    int ClassifierID { get; set; }

    //This is what the animal is called
    string AnimalName { get; set; }

    //Person who captured animal
    GameObject Catcher { get; set; }

    //Region
    Color CaughtArea { get; set; }

    //Ticks
    int NumTicks { get; set; }

    #endregion Identifying Parameters


    /// <summary>
    /// The following region contains information that will be displayed to the user either in the catch interface, tick tester, animal tester, magnifying glass, etc.
    /// Listed below are Binomial Nomenclature and identifying misc. information for the animal
    /// Some more information we might include: Habitat, behavior, conservation status, interactions with ticks, tick amount, nymph amount, larvae, color
    /// </summary>
    #region Animal Information

    //This is the "specific" name of the animal. The animal name is fox, the representative name is Red Fox. The animal name is Bunny, the representative name is Eastern Cottontail
    string representativeName { get; set; }

    int age { get; set; }

    int lifeSpan { get; set; }

    float weight { get; set; }

    float height { get; set; }

    string homeLocation { get; set; }
    
    //Aka scientific name
    string speciesName { get; set; }

    string genusName { get; set; }

    string familyName { get; set; }

    string orderName { get; set; }

    string className { get; set; }

    string phylumName { get; set; }

    string kingdomName { get; set; }

    string animalDescription { get; set; }

    string funFact { get; set; }

    #endregion Animal Information


    #region Functions
    //Despawns the given gameobject of the animal
    bool Die();
    
    #endregion Functions
}