using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HighScoreTableButton : MonoBehaviour
{
    public void Restart()

    {

        Time.timeScale = 1f;

        SceneManager.LoadSceneAsync("Test");

    }
    public void Quit()
    {

        Application.Quit();


    }

}
