using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate;
    private float spawnCounter;
    public float probability;

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0)
        {
            if(Random.value <= probability)
            {
                Instantiate(prefab, transform.position, transform.rotation);
                spawnCounter = spawnRate;
            }
            spawnCounter = spawnRate;
        }
    }
}
