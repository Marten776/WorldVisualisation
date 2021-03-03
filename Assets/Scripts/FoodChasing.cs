using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodChasing : MonoBehaviour
{
    BasicNeeds bn;
    float lerpTime = 5f;

    GameObject goal;

    AnimalMovement am;

    void Start()
    {
        bn = GetComponent<BasicNeeds>();
        am = GetComponent<AnimalMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(am.foundVictim==false)
        LookingForFood();

        if(am.foundVictim==true)
        {
            goal = LookingForFood();
            ChaisingFood(goal);
        }
    }

    public GameObject LookingForFood()
    {
        var c = Physics.OverlapBox(transform.position, new Vector3(10, 10, 10), Quaternion.identity);

        foreach(var victim in c)
        {
            if(victim.CompareTag("Rabbit"))
            {
                am.foundVictim = true;

                transform.LookAt(victim.transform.position);
                return victim.gameObject;
            }
        }
        return null;

    }

    public void ChaisingFood(GameObject food)
    {
        transform.position = Vector3.Lerp(transform.position, food.transform.position, lerpTime * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, food.transform.position);
        Debug.Log("Victim distance " + distance);
        if (distance <= 2f)
        {
            Debug.Log("Delicious vistim");
            bn.hunger = 100;
            Destroy(food.gameObject);
            am.foundVictim = false;
            return;
        }
    }
}
