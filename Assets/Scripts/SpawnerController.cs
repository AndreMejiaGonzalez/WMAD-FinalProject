using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private Manager manager;
    public GameObject prefab;
    public float spawnRate;
    public float spawnCounter;
    public float probability;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
    }

    void Update()
    {
        if(manager._state == Manager.GameState.Active)
        {
            spawnCounter -= Time.deltaTime;
            if(spawnCounter <= 0)
            {
                if(Random.value <= probability)
                {
                    Instantiate(prefab, transform.position, transform.rotation);
                }
                spawnCounter = spawnRate;
            }
        }
    }
}
