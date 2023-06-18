using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Water : MonoBehaviour
{
    public AnimationStateController animationController;

    public PlayerController myPlayer; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Water");
            animationController.isSwimming = true;
            myPlayer.isSwimming = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited water");
            animationController.isSwimming = false;
            myPlayer.isSwimming = false;
        }
    }
}