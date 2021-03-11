using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimalDied : MonoBehaviour
{
    public event Action onAnimalDied;
    private void OnDisable()
    {
        Debug.Log("Animal died");
        onAnimalDied?.Invoke();
    }
}
