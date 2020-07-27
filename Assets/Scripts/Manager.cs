using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public enum GameState
    {
        Startup,
        Active,
        Boss
    }

    public HUDController HUD;
    private ScoreKeeper keeper;
    public GameObject boss;
    public GameObject cockroachBoss;
    public Transform spawner;
    public GameState _state;
    public float startupTime;
    public int enemiesDefeated;
    public int enemiesTillDrop;
    public int enemiesTillBoss;
    public int score;
    public bool bossSpawned;
    public float globalMultiplier;

    private void Awake() {
        keeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        DontDestroyOnLoad(keeper.gameObject);
        randomizeDrop();
        Invoke("startGame", startupTime);
    }

    private void Update() {
        updateScoreKeeper();
    }

    public void Pause()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    void startGame()
    {
        _state = GameState.Active;
    }

    void updateScoreKeeper()
    {
        if(score > keeper.score)
        {
            keeper.score = score;
        }
    }

    public void randomizeDrop()
    {
        enemiesTillDrop = Random.Range(5, 20);
    }

    void spawnBoss()
    {
        if(System.DateTime.UtcNow.ToString("dd MMM") == "31 Oct")
        {
            bossSpawned = true;
            Instantiate(cockroachBoss, Vector3.zero, Quaternion.identity);
            HUD.setBossBarState(true);
            return;
        }

        if(!bossSpawned)
        {
            bossSpawned = true;
            Instantiate(boss, spawner.position, spawner.rotation);
            HUD.setBossBarState(true);
        }
    }

    public void callStartGame(float time)
    {
        Invoke("startGame", time);
    }

    public void callSpawnBoss(float time)
    {
        Invoke("spawnBoss", time);
    }

    public void changeToTitleScene()
    {
        SceneManager.LoadScene("TitleScene_0", LoadSceneMode.Single);
    }

    public void changeToGameplayScene()
    {
        keeper.score = 0;
        SceneManager.LoadScene("GameplayScene_1", LoadSceneMode.Single);
    }

    public void changeToGameOverScene()
    {
        if(score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            keeper.highestScore = PlayerPrefs.GetInt("HighScore");
        }
        SceneManager.LoadScene("GameOverScene_2", LoadSceneMode.Single);
    }

    public void endGame(float time)
    {
        Invoke("changeToGameOverScene", time);
    }
}
