using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isSwimming = animator.GetBool("isSwimming");
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
        if (!isSwimming) {
            animator.SetBool("isSwimming", true);
        }
        if (isSwimming) {
            animator.SetBool("isSwimming", false);
        }

    }
}
