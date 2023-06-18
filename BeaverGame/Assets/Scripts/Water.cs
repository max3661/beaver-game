using UnityEngine;

public class Water : MonoBehaviour
{
    public AnimationStateController animationController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Water");
            animationController.isSwimming = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited water");
            animationController.isSwimming = false;
        }
    }
}