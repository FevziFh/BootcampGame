using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    private int level = 1;
    private string sLevel;
    // Start is called before the first frame update
    public void ReplayGame()
    {
        sLevel = "Level";
        sLevel = sLevel+level;
        SceneManager.LoadScene(sLevel);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
     public void NextLevel()
    {
        level = level + 1;
        sLevel = "Level";
        sLevel = sLevel+level;
        Debug.Log(sLevel);
        SceneManager.LoadScene(sLevel);
    }
}
