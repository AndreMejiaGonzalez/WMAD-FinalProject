using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    
    // public GameObject defaultShot;
    // public GameObject waveShot;
    // public GameObject spreadShot;
    // public GameObject bombShot;
    private SpriteRenderer render;
    private Rigidbody2D rb;
    public GameObject shield;
    public GameObject defaultShot;
    public GameObject projectile;
    public Transform firePoint;
    public int lives;
    public float moveSpeed;
    public bool canFire;
    public float fireRate;
    public float fireCounter;
    public bool isHurt;
    public float iFCounter;
    public float iFTime;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        movementHandler();
        FireHandler();
        iFHandler();
        // ChangeShot();
    }

    void movementHandler()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = movement * moveSpeed;
    }

    void FireHandler()
    {
        fireCounter -= Time.deltaTime;
        if(fireCounter <= 0 && canFire)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            fireCounter = fireRate;
        }
    }

    void iFHandler()
    {
        if(isHurt)
        {
            this.gameObject.layer = 13;
            if(render.color.a == 0.5f)
            {
                render.color = new Color(render.color.r, render.color.g, render.color.b, 0);
            } else
            {
                render.color = new Color(render.color.r, render.color.g, render.color.b, .5f);
            }
            iFCounter += Time.deltaTime;
            if(iFCounter >= iFTime)
            {
                isHurt = false;
                iFCounter = 0;
                render.color = new Color(render.color.r, render.color.g, render.color.b, 1);
                this.gameObject.layer = 8;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer != 11 && other.gameObject.layer != 9)
        {
            if(shield.activeSelf == true)
            {
                shield.SetActive(false);
            } else
            {
                if(lives > 0)
                {
                    lives--;
                }
                projectile = defaultShot;
                fireRate = 0.5f;
            }
            isHurt = true;
        }
    }

    // void ChangeShot()
    // {
    //     if(Input.GetKey("1"))
    //     {
    //         projectile = defaultShot;
    //         fireRate = 0.5f;
    //     }else if(Input.GetKey("2"))
    //     {
    //         projectile = waveShot;
    //         fireRate = 0.1f;
    //     }else if(Input.GetKey("3"))
    //     {
    //         projectile = spreadShot;
    //         fireRate = 1;
    //     }else if(Input.GetKey("4"))
    //     {
    //         projectile = bombShot;
    //         fireRate = 2;
    //     }
    // }
}
