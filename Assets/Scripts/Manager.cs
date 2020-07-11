using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int enemiesDefeated;
    public int enemiesTillDrop;

    private void Awake() {
        randomizeDrop();
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

    public void randomizeDrop()
    {
        enemiesTillDrop = Random.Range(15, 20);
    }
}
