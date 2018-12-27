using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour {

    public void MM_Quit()
    {
        Application.Quit();
    }

    public void MM_Options()
    {

    }

    public void MM_CreateAWorld()
    {
        SceneManager.LoadSceneAsync("World Generation Menu");       
    }

    public void GM_Back()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void GM_Generate()
    {
        GenVarStorage GenVarStorage = GameObject.Find("Generation Variable Storage").GetComponent<GenVarStorage>();
        GameObject GenVarStorageObject = GenVarStorage.gameObject;
        int ChunkGridSize = 0;
        int.TryParse(GameObject.Find("CGT").GetComponent<Text>().text, out ChunkGridSize);
        GenVarStorage.ChunkGridSize = ChunkGridSize;
        DontDestroyOnLoad(GenVarStorageObject);
        SceneManager.LoadScene("Main World");
        SceneManager.UnloadSceneAsync("World Generation Menu");
    }

    public void PM_Continue()
    {
        GameObject GameUI = GameObject.Find("GameUI");
        GameUI.GetComponent<PauseScript>().ToggleGameState(false);       
    }

    public void PM_Options()
    {

    }

    public void PM_SQ()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
