using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseControl : MonoBehaviour
{
    // Public Variables
    public string Pause = string.Empty;
	
	// Update is called once per frame
	void Update ()
    {
        GameObject GameUI = GameObject.Find("GameUI");

        if (Input.GetKeyDown(Pause))
        {
            if (GameUI.GetComponent<PauseScript>().Paused == true)
            {
                GameUI.GetComponent<PauseScript>().ToggleGameState(false);
            }
            else
            {
                GameUI.GetComponent<PauseScript>().ToggleGameState(true);
            }            
        }
	}
}
