using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public GameObject rabbitPrefab;
    public static AnimalManager instance;
    //bool isAnimal = false;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AnimalManager in scene");
            return;
        }
        instance = this;
    }
    private GameObject currentAnimal;
    public void SetAnimal(GameObject animal)
    {
        currentAnimal = animal;
    }

    public GameObject GetWorldMatToBuild()
    {
        return currentAnimal;
    }
}
