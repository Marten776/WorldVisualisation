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
    GameObject currTree=null;
    public Vector3 pos;
    public bool isAnimalOn = false;
    public bool isPlantOn;
    public bool isWater;
    public Vector3 positionWhere;

    //bool isAnimal = false;
    void Update()
    {
        if (gameObject.CompareTag("Water"))
        {
            isWater = true;
        }

        if (firTree != null)
        {
            currTree = firTree;
            firTree = null;
        }
        if (Input.GetMouseButtonDown(0)&& currTree != null)
        {
            GameObject t = currTree;
            float scale = transform.localScale.y;
            isPlantOn = true;
            pos = new Vector3(positionWhere.x, positionWhere.y + scale, positionWhere.z);
            tre = (GameObject)Instantiate(t, pos, transform.rotation);
            
        }
        if(Input.GetMouseButtonDown(1))
        {
            currTree = null;
        }
    }
    void OnMouseDown()
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
        positionWhere = transform.position;

        //else if (firTree != null)
        //{
        //    GameObject t = firTree;
        //    float scale = transform.localScale.y;
        //    isPlantOn = true;
        //    pos = new Vector3(transform.position.x, transform.position.y + scale, transform.position.z);
        //    tre = (GameObject)Instantiate(t, pos, transform.rotation);
        //}
        //AddTree();
    }


    void AddTree()
    {
        GameObject t = firTree;
        float scale = transform.localScale.y;
        isPlantOn = true;
        pos = new Vector3(transform.position.x, transform.position.y + scale, transform.position.z);
        tre = (GameObject)Instantiate(t, pos, transform.rotation);
        firTree = null;
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
