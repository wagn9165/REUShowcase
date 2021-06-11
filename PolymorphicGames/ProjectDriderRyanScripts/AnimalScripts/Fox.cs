using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{
    void Start()
    {
        AnimalID = 0;
        AnimalName = "Fox";

        representativeName = "Red fox";
        weight = generateRandomRange(5f, 31f);
        height = generateRandomRange(14f, 20f);
        homeLocation = "Northern Hemisphere";
        speciesName = "Vulpes vulpes";
        genusName = "Vulpes";
        familyName = "Canidae";
        orderName = "Carnivora";
        className = "Mammalia";
        phylumName = "Chordata";
        kingdomName = "Animalia";
        animalDescription = "";
        funFact = "";
        

        ClassifierID = ClassifierIndex++;
    }
}
