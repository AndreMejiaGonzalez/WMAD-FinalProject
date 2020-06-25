using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform firePoint;
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
            Instantiate(projectile, firePoint.position, new Quaternion(0,0,0,0));
            fireCounter = fireRate;
        }
    }
}
