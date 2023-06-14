using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public GameObject canvasObj;
    public CinemachineFreeLook cinemachineFL;

    public float rotationSpeed;
    bool gamePaused;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Update()
    {
        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // roate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

        //Reading input for ESCAPE key, and by saying gamePaused = !gamePaused, we switch the bool on and off each time the Keycode is registered!
        if(Input.GetKeyDown(KeyCode.Escape))
            gamePaused = !gamePaused;
        
        //Now we enable and disable the game object!
        if(gamePaused) {
            canvasObj.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else {
            canvasObj.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }   

        if (canvasObj.activeInHierarchy) {   
            cinemachineFL.m_XAxis.m_MaxSpeed = 0.0f;
            cinemachineFL.m_YAxis.m_MaxSpeed = 0.0f; 
            rotationSpeed = 0;         
        }   
        else {
            cinemachineFL.m_XAxis.m_MaxSpeed = 300f;
            cinemachineFL.m_YAxis.m_MaxSpeed = 2f;
            rotationSpeed = 7;
        }
    }
}
