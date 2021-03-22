using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooSelecting : MonoBehaviour
{
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;

    }
    public void RabbitChoice()
    {
        //Debug.Log("Rabbit selected");
       buildManager.SetAnimal(buildManager.rabbitPrefab);
    }
    public void WolfChoice()
    {
        buildManager.SetAnimal(buildManager.wolfPrefab);
    }
    public void WildBoarChoice()
    {
        buildManager.SetAnimal(buildManager.wildBoarPrefab);
    }

    public void WildChickenChoice()
    {
        buildManager.SetAnimal(buildManager.wildChickenPrefab);
    }
}
