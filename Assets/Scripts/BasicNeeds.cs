using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNeeds : MonoBehaviour
{
    public int thirstiness = 100;
    public int hunger = 100;

    public int maxThirstiness;
    public int maxHunger;


    public HealthBar healthBar;
    public HungerBar hungerBar;

    void Start()
    {
        InvokeRepeating("WaterNeed", 6f, 6f);
        InvokeRepeating("FoodNeed", 15f, 15f);

        maxThirstiness = thirstiness;
        maxHunger = hunger;

        healthBar.SetMaxHealth(maxThirstiness);
        hungerBar.SetMaxHunger(maxHunger);
    }
    void WaterNeed()
    {
        thirstiness -= 5;
        healthBar.Health(thirstiness);
    }
    void FoodNeed()
    {
        hunger -= 5;
        hungerBar.Hunger(hunger);
    }
}
