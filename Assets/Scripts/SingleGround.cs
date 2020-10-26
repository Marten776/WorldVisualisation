using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGround : MonoBehaviour
{
    public Color hoverColor;

    private GameObject worldMat;
    

    private Renderer rend;
    private Color startColor;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    void OnMouseDown()
    {


       GameObject worldMata = BuildManager.instance.GetWorldMatToBuild();
       worldMat = (GameObject)Instantiate(worldMata, transform.position , transform.rotation);

    }
   
    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
