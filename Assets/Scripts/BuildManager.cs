using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    bool isCalled=false;
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public GameObject worldMatPrefab;
    public GameObject rabbitPrefab;

    void Start()
    {
        worldMata = worldMatPrefab;
    }
    void Update()
    {
        if(isCalled==true)
        {
            animals = null; 
        }
        isCalled = false;
    }
    private GameObject animals;
    private GameObject worldMata;
    public void SetAnimal(GameObject animal)
    {
        animals = animal;
    }

    public GameObject GetWorldMatToBuild()
    {
        return worldMata;
    }
    public GameObject GetAnimalToBuild()
    {
        isCalled = true;
        return animals;
    }


}
