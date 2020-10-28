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
        if(worldMat != null)
        {
            Debug.Log("cant do that");
            return;
        }

       GameObject worldMata = BuildManager.instance.GetWorldMatToBuild();

        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        worldMat = (GameObject)Instantiate(worldMata, pos , transform.rotation);

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
