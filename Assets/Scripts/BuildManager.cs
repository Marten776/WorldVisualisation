using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

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

    void Start()
    {
        worldMata = worldMatPrefab;
    }

    private GameObject worldMata;

    public GameObject GetWorldMatToBuild()
    {
        return worldMata;
    }
}
