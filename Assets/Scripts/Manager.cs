using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public enum GameState
    {
        Startup,
        Active,
        Boss
    }

    public HUDController HUD;
    public GameObject boss;
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
        randomizeDrop();
        Invoke("startGame", startupTime);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
            } else {
                Time.timeScale = 1;
            }
        }
    }

    void startGame()
    {
        _state = GameState.Active;
    }

    public void randomizeDrop()
    {
        enemiesTillDrop = Random.Range(5, 20);
    }

    void spawnBoss()
    {
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
}
