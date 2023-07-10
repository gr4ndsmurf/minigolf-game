using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void OpenURL()
    {
        Application.OpenURL("https://github.com/gr4ndsmurf/minigolf-game");
        Debug.Log("is this working?");
    }
}
