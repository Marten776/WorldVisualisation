using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNeeds : MonoBehaviour
{
    public int thirstiness = 100;
    public int hunger = 100;

    public int maxThirstiness;
    public int maxHunger;

    public SymulationSpeed symulationSpeed;

    public HealthBar healthBar;
    public HungerBar hungerBar;

    public float waterNeed = 10f;
    public float foodNeed = 15f;


    void Start()
    {
        InvokeRepeating("WaterNeed", waterNeed, waterNeed);
        InvokeRepeating("FoodNeed", foodNeed, foodNeed);

        maxThirstiness = thirstiness;
        maxHunger = hunger;

        healthBar.SetMaxHealth(maxThirstiness);
        hungerBar.SetMaxHunger(maxHunger);
    }
    public void FasterSpeed()
    {
        Debug.Log("Im hungry faster now :(!!");
        waterNeed /= 2f;
        foodNeed /= 2f;
    }

    void WaterNeed()
    {
        thirstiness -= 5;
        healthBar.Health(thirstiness);
        //Debug.Log("water need is "+waterNeed);
    }
    void FoodNeed()
    {
        hunger -= 5;
        hungerBar.Hunger(hunger);
        //Debug.Log("food need is "+ foodNeed);
    }
}
