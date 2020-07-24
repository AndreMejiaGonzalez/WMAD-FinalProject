using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public Manager manager;
    public PlayerController player;
    public BossController boss;
    public Text score;
    public Text lives;
    public GameObject bossBar;
    public Transform bossHP;

    void Update()
    {
        setScore();
        setLives();
        if(boss != null)
        {
            setSize(boss.normalizedHP);
        }
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

    public void setSize(float sizeNormalized)
    {
        bossHP.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void setBossBarState(bool state)
    {
        bossBar.SetActive(state);
    }
}