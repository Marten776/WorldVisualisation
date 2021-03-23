using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SingleGround : MonoBehaviour
{
    //public Color hoverColor;

    int counter = 0;
    GameObject worldMat;
    bool onlyOnce = true;
    private Renderer rend;
    private Color startColor;
    void Start()
    {
        //rend = GetComponent<Renderer>();
        //startColor = rend.material.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (onlyOnce==true)
            {
                CreatingGround();
                onlyOnce = false;
            }
            else
            {
                Debug.Log("You have already created your ground and you can't do that again");
                
            }
        }
    }


    void CreatingGround()
    {
        GameObject worldMata = BuildManager.instance.GetWorldMatToBuild();

            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            worldMat = Instantiate(worldMata, pos, transform.rotation);

    }
    ///void OnMouseDown()
    //{
      //  if (worldMat == null)
      //  {
       //     GameObject worldMata = BuildManager.instance.GetWorldMatToBuild();
       //    Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
      //      worldMat = (GameObject)Instantiate(worldMata, pos, transform.rotation);
      // }
   // }
   
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
      // rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
       //rend.material.color = startColor;
    }
}
