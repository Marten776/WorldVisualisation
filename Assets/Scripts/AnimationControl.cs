using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    Animator animator;
    AnimalMovement am;
    void Start()
    {
        animator = GetComponent<Animator>();
        am = GetComponent<AnimalMovement>();
        Debug.Log(animator);
    }

    
    void Update()
    {
        if (am.reachedActualCube == false)
        {
            animator.SetBool("isWalking", true);
        }
        else if (am.reachedActualCube == true)
        {
            animator.SetBool("isWalking", false);
        }



        if(am.isDying==true)
        {
            animator.SetBool("isDying", true);
        }
        else if(am.isDying==false)
        {
            animator.SetBool("isDying", false);
        }



        if(am.isEating==true)
        {
            animator.SetBool("isEating", true);
        }
        else if(am.isDying==false)
        {
            animator.SetBool("isEating", false);
        }

    }
}
