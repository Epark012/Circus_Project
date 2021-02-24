using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Singleton
#region
    public static ScoreManager instance;

    //Singletone Pattern
    public ScoreManager Instance 
    { get
        {
            if(!instance)
            {
                instance = FindObjectOfType(typeof(ScoreManager)) as ScoreManager;

                if (instance == null)
                    Debug.LogError("ERROR::SCCORE_MANAGER::NOT INITIALISED");
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    //점수
    public Text textScore;

    [SerializeField]
    private int score;

    public int SCORE
    {
        get { return score; }
        set
        {
            score = value;
            textScore.text = score.ToString();
            //만약 점수가 최고점수보다 크다면 최고점수를 점수로 갱신하고 저장(UI포함)
            if (score > highScore)
            {
                HIGHSCORE = score;

                PlayerPrefs.SetInt("HIGHSCORE", HIGHSCORE);

            }
        }
    }

    //최고점수
    public Text textHighScore;

    private int highScore = 0;
    public int HIGHSCORE
    {
        get { return highScore; }
        set
        {
            highScore = value;
            textHighScore.text = highScore.ToString();

        }
    }


    void Start()
    {
        ScoreManager.instance.SCORE = 0;
        //HIGHSCORE = PlayerPrefs.GetInt("HIGHSCORE", 0);
    }
    
    //Add points
    public void AddPoint(int point)
    {
        ScoreManager.instance.SCORE += point;
        Debug.Log(point + " is added to current points");
    }
}