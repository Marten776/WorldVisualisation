using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSelection : MonoBehaviour
{
    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void FirTreeChoice()
    {
        buildManager.SetTree(buildManager.treePrefab);
    }
    public void PoplarTreeChoice()
    {
        buildManager.SetTree(buildManager.poplarTreePrefab);
    }
    public void OakTreeChoice()
    { 
        buildManager.SetTree(buildManager.oakTreePrefab);
    }
}
