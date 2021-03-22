using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodChasing : MonoBehaviour
{
    BasicNeeds bn;
    float lerpTime = 5f;

    GameObject goal;

    AnimalMovement am;
    public SymulationSpeed symulationSpeed;

    void Start()
    {
        symulationSpeed = GetComponent<SymulationSpeed>();
        bn = GetComponent<BasicNeeds>();
        am = GetComponent<AnimalMovement>();
    }
    void Update()
    {
        if(am.foundVictim==false)
            goal = LookingForFood();

        if (am.foundVictim==true)
        {
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
        if (distance <= 2f)
        {
            bn.hunger = 100;
            Destroy(food.gameObject);
            am.foundVictim = false;
            return;
        }
    }
}
