using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHUD : MonoBehaviour
{
    private ScoreKeeper keeper;
    public Text score;
    public Text highScore;

    private void Awake() {
        keeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        setScore();
        setHighScore();
    }

    void setScore()
    {
        string tmp = "";
        string _score = keeper.score.ToString();
        for(int i = 0; i < (10 - _score.Length); i++)
        {
            tmp += "0";
        }
        score.text = tmp + _score;
    }
    
    void setHighScore()
    {
        string tmp = "";
        string _score = keeper.highestScore.ToString();
        for(int i = 0; i < (10 - _score.Length); i++)
        {
            tmp += "0";
        }
        highScore.text = tmp + _score;
    }
}
