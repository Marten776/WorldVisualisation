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
    private void Update()
    {
        if(animalMovement.animalDied==true)
        {
            ActivatePanel();
        }
    }
    private void ActivatePanel()
    {
        gameObject.SetActive(true);
    }
    private void SetText(string msg)
    {
        t.text = msg;
    }

}
