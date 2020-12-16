using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public Rigidbody rb;
    public float range = 10f;

    public string waterTag = "Water";
    public float thirstiness = 15f;

    void Start()
    {
        InvokeRepeating("waterNeed", 0f, 6f);
    }
    void WaterSpoting()
    { 
        GameObject[] water = GameObject.FindGameObjectsWithTag(waterTag);
        foreach (GameObject w in water)
        {
            Vector3 waterPosition = w.transform.position;
            float distanceToWater = Vector3.Distance(transform.position, w.transform.position);

            float drinking = 1f;
            if (distanceToWater < range)
            {
                transform.position = Vector3.MoveTowards(transform.position, waterPosition, 8f * Time.deltaTime);
                if (distanceToWater < drinking)
                {
                    thirstiness = 100f;
                }

            }
        }   
    }
    void waterNeed()
    {
        thirstiness -= 5f;
        Debug.Log("This rabbit thirstiness: " + thirstiness);
    }
    void Update()
    {

        if(thirstiness < 50f)
        {
            if (GameObject.FindGameObjectWithTag(waterTag) != null)
                WaterSpoting();
        }

        if(thirstiness == 0)
        {
            Destroy(gameObject);
            Debug.Log("The rabbit died of thirst");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
