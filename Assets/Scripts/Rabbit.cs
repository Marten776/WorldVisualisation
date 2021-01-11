using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public Rigidbody rb;
    public float range = 10f;
    public float avoidWaterRange = 1f;

    public string waterTag = "Water";
    public float thirstiness = 15f;
    private bool isWandering = false;
    private bool rotatingLeft = false;
    private bool rotatingRight = false;
    private bool isWalking = true;


    bool amIWalking = true;
    

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
                float positionX = closestWaterPosition.x;
                float positionZ = closestWaterPosition.z;
                Vector3 pos = new Vector3(positionX, transform.position.y, positionZ);
                transform.LookAt(pos);
                transform.position = Vector3.MoveTowards(transform.position, closestWaterPosition, 1f * Time.deltaTime);
                if (closestDistanceToWater < drinking)
                {
                    thirstiness = 100f;
                    Debug.Log("ahhh");
                    return; 
                }
            }
        }   
    }
    void StopWalking()
    {

        amIWalking = false;
        //StopCoroutine(Wander());
        Debug.Log("Stoped walking");
    }
    void StartWalking()
    {

        amIWalking = true;
        StartCoroutine(Wander());
        Debug.Log("Started walking");
    }

    void AvoidingWater()
    {
        
        GameObject[] water = GameObject.FindGameObjectsWithTag(waterTag);
        Vector3 closestWaterPos = new Vector3(0, 0, 0);
        float closestDistanceToWater = Mathf.Infinity;

        foreach (GameObject w in water)
        {
            Vector3 waterPosition = w.transform.position;
            Vector3 rabbitPosition = transform.position;
            Vector3 goThere = new Vector3(0, 0, 0);
            float distanceToWater = Vector3.Distance(transform.position, w.transform.position);
            if (distanceToWater < closestDistanceToWater)
            {
                closestDistanceToWater = distanceToWater;
                closestWaterPos = waterPosition;
            }
            float avoid = 1.5f;
            if (closestDistanceToWater < avoid)
            {
                Debug.Log("Im here");
                StopWalking();
                
                float newPosition = transform.position.y + 180f;
                Vector3 newRotation = new Vector3(transform.rotation.x, newPosition, transform.rotation.z);
                transform.Rotate(new Vector3(0f,1f,0f), 50f * Time.deltaTime);
                if (transform.rotation.y > 0.9)
                {
                    transform.Rotate(new Vector3(0f, 0f, 0f), 0f * Time.deltaTime);
                    float newDirection = transform.position.z - 1f;
                    goThere = new Vector3(transform.position.x, transform.position.y, newDirection);
                    transform.position = Vector3.MoveTowards(transform.position, goThere, 1f * Time.deltaTime);

                }
            }
            if (closestDistanceToWater > avoid)
            {
                amIWalking = true;
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
        if (amIWalking == true)
        {
            Walking();
        }

        if (thirstiness <= 50f)
        {

            if (GameObject.FindGameObjectWithTag(waterTag) != null)
                WaterSpoting();
        }
        else if(thirstiness > 50f)
        {
           // Collider[] waterColliders = Physics.OverlapSphere(GameObject.FindGameObjectWithTag(waterTag).transform.position, range);
            //if (waterColliders.Length != 0)
                AvoidingWater();
        }
        else if(thirstiness == 0)
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
