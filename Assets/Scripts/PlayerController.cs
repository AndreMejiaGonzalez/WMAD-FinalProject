using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Manager manager;
    public Joystick joystick;
    private SpriteRenderer render;
    private Rigidbody2D rb;
    public GameObject shield;
    public GameObject explode;
    public GameObject defaultShot;
    public GameObject projectile;
    public Transform firePoint;
    public int lives;
    public float moveSpeed;
    public float fireRate;
    private float fireCounter;
    public bool isHurt;
    private float iFCounter;
    public float iFTime;
    public bool isDead;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movementHandler();
        FireHandler();
        iFHandler();
    }

    void movementHandler()
    {   
        rb.velocity = joystick.Direction * moveSpeed;
    }

    void FireHandler()
    {
        if(!isDead)
        {
            fireCounter -= Time.deltaTime;
            if(fireCounter <= 0)
            {
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                fireCounter = fireRate;
            }
        }
    }

    void iFHandler()
    {
        if(isHurt && !isDead)
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

    void Die()
    {
        if(lives == 0)
        {
            isDead = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            render.color = new Color(255, 0, 0, 1);
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 3);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer != 11 && other.gameObject.layer != 9 && !isDead)
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
                fireRate = 0.25f;
            }
            isHurt = true;
            Die();
        }
    }

    private void OnDestroy() {
        manager.changeToGameOverScene();
    }
}
