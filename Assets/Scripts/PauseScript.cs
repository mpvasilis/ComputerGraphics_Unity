using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    // Public Varibles
    public bool Paused = false;

    public void ToggleGameState(bool PauseGame)
    {
        // Check if the game is going to be unpaused
        if (PauseGame == false)
        {
            // Lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
        }
        // Set the Paused Variable to the desired bool
        Paused = PauseGame;
        // Find and enable the Pause Menu
        GameObject GameUI = GameObject.Find("GameUI");
        GameUI.transform.Find("Pause Menu").gameObject.SetActive(PauseGame);        
    }
}
