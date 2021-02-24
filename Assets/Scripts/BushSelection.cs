using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushSelection : MonoBehaviour
{
    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void FirstBushChoice()
    {
        buildManager.SetBush(buildManager.firstBushPrefab);
    }
    public void SecoundBushChoice()
    {
        buildManager.SetBush(buildManager.secoundBushPrefab);
    }
    public void ThirdBushChoice()
    {
        buildManager.SetBush(buildManager.thirdBushPrefab);
    }
    public void FourthBushChoice()
    {
        buildManager.SetBush(buildManager.fourthBushPrefab);
    }
    public void FifthBushChoice()
    {
        buildManager.SetBush(buildManager.fifthBushPrefab);
    }
}
