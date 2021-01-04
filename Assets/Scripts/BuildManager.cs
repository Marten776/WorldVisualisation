using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    //bool isAnimal = false;
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


    private GameObject worldMata;
    //public void SetAnimal(GameObject animal)
   // {
   //     worldMata = animal;
   // }

    public GameObject GetWorldMatToBuild()
    {
        return worldMata;
    }


}
