using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int highestScore;
    public int score;

    private void Awake() {
        highestScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
