using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoneAnimalPanel : MonoBehaviour
{
    public Text t;
    public AnimalMovement animalMovement { get; private set; }
    private void Awake()
    {
        animalMovement = GetComponent<AnimalMovement>();
    }
    private void OnEnable()
    {
        animalMovement.onAnimalDied += ActivatePanel; 
    }
    private void OnDisable()
    {
        animalMovement.onAnimalDied -= ActivatePanel;
    }
    private void ActivatePanel()
    {
        Debug.Log("I know");
        gameObject.SetActive(true);
    }
    private void SetText(string msg)
    {
        t.text = msg;
    }

}
