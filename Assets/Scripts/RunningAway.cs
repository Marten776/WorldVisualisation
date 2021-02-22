using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAway : MonoBehaviour
{

    float lerpTime = 1f;

    AnimalMovement am;
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<AnimalMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (am.foundVictim == false)
        LookingForEnemy();
    }

    void LookingForEnemy()
    {
        var c = Physics.OverlapBox(transform.position, new Vector3(10, 10, 10), Quaternion.identity);

        foreach(var enemy in c)
        {
            if(enemy.CompareTag("Danger"))
            {
                am.foundVictim = true;
                Vector3 position = enemy.transform.position;
                position = transform.InverseTransformDirection(enemy.transform.position);
                transform.LookAt(position);
                transform.position = Vector3.MoveTowards(transform.position, position, lerpTime *Time.deltaTime);
            }
        }
    }
}
