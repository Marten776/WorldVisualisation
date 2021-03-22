using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public bool isCalled=false;
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
    public GameObject wolfPrefab;
    public GameObject wildBoarPrefab;
    public GameObject wildChickenPrefab;



    public GameObject treePrefab;
    public GameObject poplarTreePrefab;
    public GameObject oakTreePrefab;

    public GameObject firstBushPrefab;
    public GameObject secoundBushPrefab;
    public GameObject thirdBushPrefab;
    public GameObject fourthBushPrefab;
    public GameObject fifthBushPrefab;

    GameObject worldMata;
    void Start()
    {
        worldMata = worldMatPrefab;
    }
    void Update()
    {
        if(isCalled==true)
        {
            //animals = null;
            // trees = null;
            // bushes = null;
        }
        if (Input.GetKeyDown("c"))
        {
            bushes = null;
            animals = null;
            trees = null;
        }
        isCalled = false;
    }
    private GameObject animals;
    private GameObject trees;
    private GameObject bushes;

    public void SetAnimal(GameObject animal)
    {
        animals = animal;
    }

    public void SetTree(GameObject tree)
    {
        trees = tree;
    }    
    
    public void SetBush(GameObject bush)
    {
        bushes = bush;
    }

    public GameObject GetTreeToBuild()
    {
        isCalled = true;
        return trees;
    }

    public GameObject GetBushToBuild()
    {
        return bushes;
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
