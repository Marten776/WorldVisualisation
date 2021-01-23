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
    }
    void Movement()
    {
        Collider[] ground = Physics.OverlapSphere(transform.position, 5f);

        myPos = transform.position;

        if (thirstiness <= 50)
        {
            //Debug.Log("Im thirsty for fuck sake");
            foreach (Collider p in ground)
            {
                if (p.CompareTag("Water"))
                {
                    Debug.Log("Yaaaay! Found some water");
                    //waterDirection = p.transform.position;
                    float WaterScale = transform.localScale.y;
                    Vector3 waterDirection = new Vector3(p.transform.position.x, p.transform.position.y + WaterScale, p.transform.position.z);
                    Debug.Log("Water pleace " + waterDirection);
                    transform.LookAt(waterDirection);
                   if (Vector3.Distance(myPos, waterDirection) > 1f)
                       {

                        transform.position = Vector3.Lerp(myPos, waterDirection, lerpTime * Time.deltaTime);
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
                //newDir = currentGoal.transform.position;

                float newDirScale = transform.localScale.y;
                newDir= new Vector3(currentGoal.transform.position.x, currentGoal.transform.position.y + newDirScale, currentGoal.transform.position.z);
                transform.LookAt(newDir);
            }
        }
        if (Vector3.Distance(myPos, newDir) > .1f)
        {
            transform.position = Vector3.Lerp(myPos, newDir, lerpTime * Time.deltaTime);
            Debug.Log(newDir);
        }
    }
    void waterNeed()
    {
        thirstiness -= 10f;
        Debug.Log("Rabbit thirstines level is " + thirstiness);
    }
}
