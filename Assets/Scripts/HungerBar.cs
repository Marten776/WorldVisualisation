using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHunger(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void Hunger(int health)
    {
        slider.value = health;
    }

}
