using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject 일시정지;
    public string thisScene;

    // Use this for initialization

    void Start()
    {
        thisScene = SceneManager.GetActiveScene().name;  // 실행 중인 씬의 이름을 가져온다
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))   // Esc키를 누르면
        {
            Time.timeScale = 0f;
            일시정지.SetActive(true);
        }
    }

    public void 재시작()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(thisScene);
    }

    public void 계속하기()
    {
        일시정지.SetActive(false);
        Time.timeScale = 1f;
    }

    public void 게임종료()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("HighScoreboard");
    }

    public void 메인으로()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Main");
    }

}


