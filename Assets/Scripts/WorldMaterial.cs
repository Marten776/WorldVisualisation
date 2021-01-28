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
    //bool isAnimal = false;
    private void Update()
    {
        // onAnimalSelected();
        //animal = null;
        
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        animal = BuildManager.instance.GetAnimalToBuild();
        firTree = BuildManager.instance.GetTreeToBuild();
        if (animal != null)
        {
            Debug.Log("Welcome animal is not null");
            GameObject rabbit = animal;
            float scale = transform.localScale.y;
            pos = new Vector3(transform.position.x, transform.position.y + scale, transform.position.z);
            rab = (GameObject)Instantiate(rabbit, pos, transform.rotation);
        }
        else if (firTree != null)
        {
            Debug.Log("Welcome firTree is not null");
            GameObject t = firTree;
            float scale = transform.localScale.y;
            pos = new Vector3(transform.position.x, transform.position.y + scale, transform.position.z);
            tre = (GameObject)Instantiate(t, pos, transform.rotation);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rabbit")
        {
            //Debug.Log("Some rabbit is on block with this position: " + transform.position);
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
