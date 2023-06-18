using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    public bool isSwimming;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isSwimming = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool swimCheck = animator.GetBool("swimCheck");
        bool pressedMovementKey = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right");

        //if player presses w key
        if (!isWalking && pressedMovementKey) {

            //then set the isWalking boolean to be true
            animator.SetBool("isWalking", true);
        }

        //if player is not pressing w key
        if (isWalking && !pressedMovementKey) {

            //then set the isWalking boolean to be false
            animator.SetBool("isWalking", false);

        }

        //swimming
        if (!swimCheck && isSwimming) {
            animator.SetBool("swimCheck", true);
        }
        else if (swimCheck && !isSwimming) {
            animator.SetBool("swimCheck", false);
        }

    }
}
