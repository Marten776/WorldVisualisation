using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public Rigidbody rb;
    public float range = 10f;

    public string waterTag = "Water";
    public float thirstiness = 15f;
    private bool isWandering = false;
    private bool rotatingLeft = false;
    private bool rotatingRight = false;
    private bool isWalking = false;



    void Start()
    {
        InvokeRepeating("waterNeed", 6f, 6f);
    }
    void WaterSpoting()
    { 
        GameObject[] water = GameObject.FindGameObjectsWithTag(waterTag);
        Vector3 closestWaterPosition = new Vector3(0, 0, 0);
        float closestDistanceToWater = Mathf.Infinity;
        foreach (GameObject w in water)
        {
            Vector3 waterPosition = w.transform.position;
            float distanceToWater = Vector3.Distance(transform.position, w.transform.position);
            if(distanceToWater < closestDistanceToWater)
            {
                closestDistanceToWater = distanceToWater;
                closestWaterPosition = waterPosition;
            }
            float drinking = 1.5f;
            if (closestDistanceToWater < range)
            {
                transform.LookAt(closestWaterPosition);
                transform.position = Vector3.MoveTowards(transform.position, closestWaterPosition, 2f * Time.deltaTime);
                if (closestDistanceToWater < drinking)
                {
                    thirstiness = 100f;
                }
            }
        }   
    }

    void Walking()
    {
        if(isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (rotatingLeft == true)
        {
            transform.Rotate(transform.up * 100 * Time.deltaTime);
        }
        if (rotatingRight == true)
        {
            transform.Rotate(transform.up * -100 * Time.deltaTime);
        }
        if(isWalking == true)
        {
            transform.position += transform.forward * 2f * Time.deltaTime;
        }

    }
    void waterNeed()
    {
        thirstiness -= 5f;
        //Debug.Log("This rabbit thirstiness: " + thirstiness);
    }
    void Update()
    {

        Walking();

        if (thirstiness < 50f)
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
    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1,3);
        int rotLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1,3);
        int walkTime = Random.Range(1, 5);

        isWandering = true;
        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if(rotLorR == 1)
        {
            rotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            rotatingRight = false;
        }
        if (rotLorR == 2)
        {
            rotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            rotatingLeft = false;
        }
        isWandering = false;
    }
}
