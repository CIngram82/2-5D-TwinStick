using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

    public bool recording = true;
    public GameObject player;

    private float fixedDeltaTime;
    private bool isPaused = false;

    private void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        print("fixed delta time: " + fixedDeltaTime);
    }

    void Update () {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            recording = false;
        }
        else
        {
            recording = true;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                PauseGame();
                isPaused = true;
            }
            else
            {
                UnPauseGame();
                isPaused = false;
            }
        }    
	}
    void PauseGame()
    {
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = 0.0f;
    }
    void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = fixedDeltaTime;
    }
    private void OnApplicationPause(bool pause)
    {
        isPaused = pause;
        PauseGame();
    }

}
