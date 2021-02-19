using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodChasing : MonoBehaviour
{
    BasicNeeds bn;
    HungerBar hb;
    float lerpTime = 5f;

    AnimalMovement am;

    void Start()
    {
        bn = GetComponent<BasicNeeds>();
        am = GetComponent<AnimalMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        LookingForFood();
    }

    public void LookingForFood()
    {
        var c = Physics.OverlapBox(transform.position, new Vector3(10, 10, 10), Quaternion.identity);

        foreach(var victim in c)
        {
            if(victim.CompareTag("Rabbit"))
            {
                am.foundVictim = true;

                transform.LookAt(victim.transform.position);
                transform.position = Vector3.Lerp(transform.position, victim.transform.position, lerpTime * Time.deltaTime);
                float distance = Vector3.Distance(transform.position, victim.transform.position);
                Debug.Log("Victim distance " + distance);
                if(distance<=2f)
                {
                    Debug.Log("Delicious vistim");
                    bn.hunger = 100;
                    Destroy(victim.gameObject);
                    am.foundVictim = false;
                    hb.Hunger(100);
                    return;
                }
               
            }
        }

    }
}
