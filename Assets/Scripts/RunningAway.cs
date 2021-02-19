using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAway : MonoBehaviour
{

    float lerpTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookingForEnemy();
    }

    void LookingForEnemy()
    {
        var c = Physics.OverlapBox(transform.position, new Vector3(10, 10, 10), Quaternion.identity);

        foreach(var enemy in c)
        {
            if(enemy.CompareTag("Danger"))
            {
                Vector3 position = new Vector3(enemy.transform.position.x, -enemy.transform.position.y, enemy.transform.position.z);
                transform.LookAt(position);
                transform.position = Vector3.Lerp(transform.position, position, lerpTime * Time.deltaTime);
            }
        }
    }
}
