using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymulationSpeed : MonoBehaviour
{

    public void FastSpeed()
    {
        Time.timeScale = 2f;
    }
    public void SlowSpeed()
    {
        Time.timeScale = 0.5f;
    }
    public void NormalSpeed()
    {
        Time.timeScale = 1f;
    }

}
