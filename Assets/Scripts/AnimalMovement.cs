using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    float timeStartedLerping;
    bool shouldLerp = false;
    float lerpTime = 2f;
    float waterSearchLerpTime = 5f;
    Vector3 myPos;
    Vector3 newDir;
    public float thirstiness = 70f;
    private void Start()
    {
        myPos = transform.position;
        newDir = transform.position;
        InvokeRepeating("waterNeed", 6f, 6f);
    }
    void Update()

    {
        Movement();


        if(thirstiness == 0f)
        {
            Destroy(gameObject);
        }

        //if (thirstiness < 50f)
        //    WaterSearch();
    }
    void Movement()
    {
        Collider[] ground = Physics.OverlapSphere(transform.position, 5f);
        myPos = transform.position;
        if (thirstiness <= 50)
        {
            //Debug.Log("Im thirsty for fuck sake");
            foreach (Collider x in ground)
            {
                if (x.CompareTag("Water"))
                {
                    Debug.Log("Yaaaay! Found some water");
                    newDir = x.transform.position;
                    newDir += Vector3.up;
                    transform.LookAt(newDir);
                    Vector3 newDirection = newDir - ((myPos - newDir) * 1.5f);
                    if (Vector3.Distance(myPos, newDir) < 1.5f)
                       {
                        transform.position = Vector3.Lerp(myPos, newDirection, lerpTime * Time.deltaTime);
                        thirstiness = 100f;
                        Debug.Log("ahhhhh");
                        return;
                      }
                }
            }
        }

        if (Vector3.Distance(myPos, newDir) < .1f)
        {
            int elementNumber = Random.Range(0, ground.Length);
            var currentGoal = ground[elementNumber];
            if (currentGoal.tag == "Selectable")
            {
                newDir = currentGoal.transform.position;
                newDir += Vector3.up;
                transform.LookAt(newDir);
            }
        }
        if (Vector3.Distance(myPos, newDir) > .1f)
        {
            transform.position = Vector3.Lerp(myPos, newDir, lerpTime * Time.deltaTime);
        }
    }

    void WaterSearch()
    {
        Collider[] ground = Physics.OverlapSphere(transform.position, 4f);
        myPos = transform.position;
        foreach (Collider x in ground)
        {
            if (x.CompareTag("Water"))
            {
                newDir = x.transform.position;
                newDir += Vector3.up;
                transform.LookAt(newDir);
                thirstiness = 100f;
                transform.position = Vector3.Lerp(myPos, newDir, waterSearchLerpTime * Time.deltaTime);
                if (Vector3.Distance(myPos, newDir) > .5f)
                    return;
            }
        }
        if (Vector3.Distance(myPos, newDir) < .5f)
        {
            int elementNumber = Random.Range(0, ground.Length);
            var currentGoal = ground[elementNumber];
            if (currentGoal.tag == "Selectable")
            {
                newDir = currentGoal.transform.position;
                newDir += Vector3.up;
                transform.LookAt(newDir);
            }
        }
        if (Vector3.Distance(myPos, newDir) > .5f)
        {
            transform.position = Vector3.Lerp(myPos, newDir, lerpTime * Time.deltaTime);
        }
    }
    void waterNeed()
    {
        thirstiness -= 10f;
        Debug.Log("Rabbit thirstines level is " + thirstiness);
    }
}
