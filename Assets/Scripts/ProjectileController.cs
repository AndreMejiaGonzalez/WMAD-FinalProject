using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject explode;
    public float shotSpeed;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        rb.velocity = this.transform.up * shotSpeed;
        Destroy(this.gameObject, 3);
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
