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
        buildManager.SetTree(buildManager.firstBushPrefab);
    }
    public void SecoundBushChoice()
    {
        buildManager.SetTree(buildManager.secoundBushPrefab);
    }
    public void ThirdBushChoice()
    {
        buildManager.SetTree(buildManager.thirdBushPrefab);
    }
    public void FourthBushChoice()
    {
        buildManager.SetTree(buildManager.fourthBushPrefab);
    }
    public void FifthBushChoice()
    {
        buildManager.SetTree(buildManager.fifthBushPrefab);
    }
}
