using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CodeMonkey.Utils;

public class SelectingController : MonoBehaviour
{
    private Vector3 startPosition;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           // startPosition = UtilsClass.GetMouseWorldPosition();
        }
        if(Input.GetMouseButtonUp(0))
        {
            //Debug.Log(UtilsClass.GetMouseWorldPosition() + " " + startPosition);
        }
    }
}
