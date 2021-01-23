using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorldMaterial : MonoBehaviour
{
    private GameObject rab;
    GameObject animal;
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
        if (animal != null)
        {
            GameObject rabbit = animal;
            float scale = transform.localScale.y;
            pos = new Vector3(transform.position.x, transform.position.y + scale, transform.position.z);
            rab = (GameObject)Instantiate(rabbit, pos, transform.rotation);
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
