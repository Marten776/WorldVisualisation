using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public Rigidbody rb;
    void Update()
    {

        rb.AddForce(0, 0, 1200 * Time.deltaTime);

    }
}
