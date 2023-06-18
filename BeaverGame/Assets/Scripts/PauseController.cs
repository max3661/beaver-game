using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject canvasObj;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            GameIsPaused = !GameIsPaused;

            if(GameIsPaused)
                Pause();
            else if (!GameIsPaused)
                Resume();
    }

    public void Resume() {
        Time.timeScale = 1f;
        GameIsPaused = false; 

        canvasObj.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void Pause() {

        Time.timeScale = 0f;
        GameIsPaused = true;

        canvasObj.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DoQuit() {
        Application.Quit(); 
    }
}
