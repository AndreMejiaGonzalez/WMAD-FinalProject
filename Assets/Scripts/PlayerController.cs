using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform firePoint;
    public GameObject defaultShot;
    public GameObject waveShot;
    public GameObject spreadShot;
    public GameObject bombShot;
    public GameObject projectile;
    public float moveSpeed;
    public float fireRate;
    public float fireCounter;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        movementHandler();
        FireHandler();
        ChangeShot();
    }

    void movementHandler()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = movement * moveSpeed;
    }

    void FireHandler()
    {
        fireCounter -= Time.deltaTime;
        if(fireCounter <= 0)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            fireCounter = fireRate;
        }
    }

    void ChangeShot()
    {
        if(Input.GetKey("1"))
        {
            projectile = defaultShot;
            fireRate = 0.5f;
        }else if(Input.GetKey("2"))
        {
            projectile = waveShot;
            fireRate = 0.1f;
        }else if(Input.GetKey("3"))
        {
            projectile = spreadShot;
            fireRate = 1;
        }else if(Input.GetKey("4"))
        {
            projectile = bombShot;
            fireRate = 2;
        }
    }
}
