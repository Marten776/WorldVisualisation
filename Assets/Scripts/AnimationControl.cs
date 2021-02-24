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


        if(am.canWalk==false)
        {
            animator.SetBool("isWalking", false);
        }

        if (am.isDying == true)
        { 
            animator.SetBool("isDying", true);
        }



        //if(am.isEating==true)
        //{
        //    animator.SetBool("isEating", true);
        //}
        //else if(am.isEating==false)
        //{
        //    animator.SetBool("isEating", false);
        //}


        if (am.foundVictim == true)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRuning", true);
        }
        else if (am.foundVictim == false)
        {
            animator.SetBool("isRuning", false);
            if (am.isDying == false && am.isResting == false)
            {
                animator.SetBool("isWalking", true);
            }
        }
    }
}
