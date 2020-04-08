using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("BlendJumpWalk", 1.0f);
        animator.SetFloat("BlendJumpRun", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isWalkingBackwards", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingBackwards", false);
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isWalking", false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", false);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("isJumping", false);
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isWalkingBackwards", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);
        }


        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Rotate(0.0f, 5.0f, 0.0f);
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Rotate(0.0f, -5.0f, 0.0f);
            animator.SetBool("isWalking", true);
        }
    }
}
