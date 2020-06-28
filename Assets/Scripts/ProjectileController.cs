using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject explode;
    public float shotSpeed;
    public float duration;

    private void Awake() {
        Destroy(this.gameObject, duration);
    }

    void Update()
    {
        transform.position += this.transform.up * shotSpeed * Time.deltaTime;
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(this.gameObject.tag == "BombShot")
        {
            Instantiate(explode, transform.position, transform.rotation);
        }
        if(this.gameObject.tag != "WaveShot")
        {
            Destroy(this.gameObject);
        }
    }
}
