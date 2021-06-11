using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureEvents : MonoBehaviour
{
    public delegate void AnimalEventHandler(IAnimal animal);
    public static event AnimalEventHandler OnAnimalCapture;

    public static void AnimalCaptured(IAnimal animal)
    {
        OnAnimalCapture?.Invoke(animal);
    }
}
