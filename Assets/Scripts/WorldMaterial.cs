using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorldMaterial : MonoBehaviour
{
    private GameObject rab;
    private GameObject tre;
    GameObject animal;
    GameObject firTree;
    public Vector3 pos;
    public bool isAnimalOn=false;
    public bool isPlantOn;
    public bool isWater;
    //bool isAnimal = false;
    private void Update()
    {
        if(gameObject.CompareTag("Water"))
        {
            isWater = true;
        }
        if (gameObject.CompareTag("Bush"))
        {
            isPlantOn = true;
        }
        if(gameObject.CompareTag("Selectable"))
        {
            isPlantOn = false;
            isWater = false;

        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        animal = BuildManager.instance.GetAnimalToBuild();
        firTree = BuildManager.instance.GetTreeToBuild();
        if (animal != null)
        {
            GameObject rabbit = animal;
            float scale = transform.localScale.y;
            pos = new Vector3(transform.position.x, transform.position.y + scale, transform.position.z);
            rab = (GameObject)Instantiate(rabbit, pos, transform.rotation);
        }
        else if (firTree != null)
        {
            GameObject t = firTree;
            float scale = transform.localScale.y;
            gameObject.tag = "Bush";
            pos = new Vector3(transform.position.x, transform.position.y + scale, transform.position.z);
            tre = (GameObject)Instantiate(t, pos, transform.rotation);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {


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
