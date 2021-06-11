using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bunny : Animal
{
    void Start()
    {
        AnimalID = 1;
        AnimalName = "Bunny";

        representativeName = "Eastern Cottontail";
        weight = generateRandomRange(1f, 3f);
        height = generateRandomRange(7f, 9f);
        homeLocation = "North America";
        speciesName = "Sylvilagus floridanus";
        genusName = "Sylvilagus";
        familyName = "Leporidae";
        orderName = "Lagomorpha";
        className = "Mammalia";
        phylumName = "Chordata";
        kingdomName = "Animalia";
        animalDescription = "";
        funFact = "";

        ClassifierID = ClassifierIndex++;
    }

}
