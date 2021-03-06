﻿using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;

    public float panSpeed = 20f;
    public float rotateSpeed = 100f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 10f;
    public float minY = 2f;
    public float maxY = 1000f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
            doMovement = !doMovement;
        if (!doMovement)
            return;

        if(Input.GetKey("w") )
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey("q"))
        {
          transform.Rotate(0, rotateSpeed * Time.deltaTime,0, Space.World);
        }
        if (Input.GetKey("e"))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0, Space.World);
        }
        if (Input.GetKey("s") )
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime);
        }        
        if(Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
        }        
        if(Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
