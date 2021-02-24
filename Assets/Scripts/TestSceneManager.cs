using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TestSceneManager : MonoBehaviour
{
    public string thisScene;

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Test");
    }
    public void Save()
    {
        SceneManager.LoadScene("HighScoreBoard");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}




