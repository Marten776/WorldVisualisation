using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAway : MonoBehaviour
{

    float lerpTime = 1f;

    AnimalMovement am;
    // Start is called before the first frame update
    public SymulationSpeed symulationSpeed;
    void Start()
    {
        symulationSpeed = GetComponent<SymulationSpeed>();
        am = GetComponent<AnimalMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (am.foundVictim == false)
        LookingForEnemy();

        if (am.foundVictim == true)
        RunAway(LookingForEnemy());
    }

    Vector3 LookingForEnemy()
    {
        var c = Physics.OverlapBox(transform.position, new Vector3(10, 10, 10), Quaternion.identity);
        Vector3 position = new Vector3(0, 0, 0);
        foreach(var enemy in c)
        {
            if(enemy.CompareTag("Danger"))
            {
                am.foundVictim = true;
                
                //position = enemy.gameObject.transform.position - enemy.transform.forward*10 + transform.position;
               position = transform.position - enemy.transform.position;
                transform.LookAt(position);
                return position;
            }
        }
        return position; 

    }
    void RunAway(Vector3 escape)
    {
        transform.position = Vector3.Lerp(transform.position, escape, lerpTime * Time.deltaTime);
    }
}
