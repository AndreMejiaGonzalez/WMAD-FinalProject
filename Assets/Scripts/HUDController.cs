using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public Manager manager;
    public PlayerController player;
    public Text score;
    public Text lives;

    void Update()
    {
        setScore();
        setLives();
    }

    void setScore()
    {
        string tmp = "";
        string currentScore = manager.score.ToString();
        for(int i = 0; i < (10 - currentScore.Length); i++)
        {
            tmp += "0";
        }
        score.text = tmp += currentScore;
    }

    void setLives()
    {
        string tmp = "";
        string currentLives = player.lives.ToString();
        for(int i = 0; i < (2 - currentLives.Length); i++)
        {
            tmp += "0";
        }
        lives.text = ("x" + tmp + currentLives);
    }
}