using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorldMaterial : MonoBehaviour
{
    private GameObject rab;
    private GameObject tre;
    private GameObject bus;
    GameObject animal;
    GameObject tree;
    GameObject bush;
    public Vector3 pos;
    public bool isAnimalOn = false;
    public bool isPlantOn;
    public bool isTreeOn;
    public bool isWater;
    public Vector3 positionWhere;
    //bool isAnimal = false;
    void Update()
    {
        if (gameObject.CompareTag("Water"))
        {
            isWater = true;
        }
        else if(!gameObject.CompareTag("Water"))
        {
            isWater = false;
        }

    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        animal = BuildManager.instance.GetAnimalToBuild();
        tree = BuildManager.instance.GetTreeToBuild();
        bush = BuildManager.instance.GetBushToBuild();
        if (animal != null)
        {
            GameObject currentAnimal = animal;
            float scale = transform.localScale.y;
            pos = new Vector3(transform.position.x, transform.position.y + scale, transform.position.z);
            Instantiate(currentAnimal, pos, transform.rotation);
        }
        if (bush != null)
        {
            GameObject currentBush = bush;
            float scale = transform.localScale.y;
            isPlantOn = true;
            pos = new Vector3(transform.position.x, transform.position.y + scale, transform.position.z);
            Instantiate(currentBush, pos, transform.rotation);
        }
        if (tree != null)
        {
            GameObject currentTree = tree;
            float scale = transform.localScale.y;
            isTreeOn = true;
            pos = new Vector3(transform.position.x, transform.position.y + scale, transform.position.z);
            Instantiate(currentTree, pos, transform.rotation);
        }

    }

    void OnMouseEnter()
    {
        //rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        //rend.material.color = startColor;
    }
}
