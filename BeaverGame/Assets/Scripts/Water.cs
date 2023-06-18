using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Water : MonoBehaviour
{

    public PlayerController myPlayer; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Water");
            myPlayer.isSwimming = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited water");
            myPlayer.isSwimming = false;
        }
    }
}