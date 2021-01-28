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
        //Debug.Log("Fir Tree Choiced");
        buildManager.SetTree(buildManager.treePrefab);
    }
    public void PoplarTreeChoice()
    {
        //Debug.Log("Fir Tree Choiced");
        buildManager.SetTree(buildManager.poplarTreePrefab);
    }
    public void OakTreeChoice()
    {
        //Debug.Log("Fir Tree Choiced");
        buildManager.SetTree(buildManager.oakTreePrefab);
    }
}
