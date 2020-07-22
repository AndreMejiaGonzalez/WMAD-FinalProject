using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Manager manager;
    private Transform playerPos;
    private SpriteRenderer render;
    public Transform[] firePoints;
    public GameObject projectile;
    public GameObject drop;
    public GameObject scoreSprite;
    public float hp;
    public float moveSpeed;
    public float rotationSpeed;
    private float rotationCounter;
    private Vector2 right;
    private Vector2 left;
    private Vector2 rotateSide;
    public float fireRate;
    private float fireCounter;
    private Color normalColor;
    private Color redColor;
    public int scoreYield;

    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        render = this.gameObject.GetComponent<SpriteRenderer>();
        fireCounter = fireRate;
        normalColor = new Color(255, 255, 255, 1);
        redColor = new Color(255, 0, 0, 1);
        rotationCounter = rotationSpeed;
        right = Vector2.right;
        left = -Vector2.right;
        if(Random.value <= .5f)
        {
            rotateSide = right;
        } else
        {
            rotateSide = left;
        }
    }

    void Update()
    {
        if(this.gameObject.tag == "W-type")
        {
            W_TypeHandler();
        } else if(this.gameObject.tag == "S-type")
        {
            S_TypeHandler();
        } else if(this.gameObject.tag == "B-type")
        {
            B_TypeHandler();
        } else if(this.gameObject.tag == "Sh-type")
        {
            Sh_TypeHandler();
        } else if(this.gameObject.tag == "L-type")
        {
            L_TypeHandler();
        }

        if(hp <= 0)
        {
            Die();
        }

        FireHandler();
    }

    void W_TypeHandler()
    {
        if(transform.position.x - playerPos.position.x > 0 &&
        transform.position.x - playerPos.position.x < 1 &&
        playerPos.position.y < transform.position.y)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    void S_TypeHandler()
    {
        rotationCounter -= Time.deltaTime;
        if(rotationCounter <= 0)
        {
            transform.Rotate(0,0,20);
            rotationCounter = rotationSpeed;
        }
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);
    }

    void B_TypeHandler()
    {
        rotationCounter -= Time.deltaTime;
        if(rotationCounter <= 0)
        {
            if(rotateSide == left)
            {
                rotateSide = right;
            } else {
                rotateSide = left;
            }
            rotationCounter = rotationSpeed;
        }
        transform.Translate((Vector2.down + rotateSide) * moveSpeed * Time.deltaTime, Space.World);
    }

    void Sh_TypeHandler()
    {
        if(!(transform.position.y <= 4))
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        } else {
            if(transform.position.x >= 10)
            {
                rotateSide = left;
            } else if(transform.position.x <= -10)
            {
                rotateSide = right;
            }
            transform.position += new Vector3(rotateSide.x, rotateSide.y, 0) *moveSpeed * Time.deltaTime;
        }
    }

    void L_TypeHandler()
    {
        if(!(transform.position.y <= 0))
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        } else {
            if(rotationCounter > 0)
            {
                Vector3 target = playerPos.position - transform.position;
                transform.up = new Vector3(target.x, target.y, 0);
                rotationCounter -= Time.deltaTime;
            } else {
                transform.position += transform.up * moveSpeed * Time.deltaTime;
            }
        }
    }

    void FireHandler()
    {
        if(this.gameObject.GetComponent<Renderer>().isVisible)
        {
            if(firePoints.Length > 0)
            {
                fireCounter -= (Time.deltaTime * manager.globalMultiplier);
                if(fireCounter <= 0)
                {
                    foreach(Transform firePoint in firePoints)
                    {
                        Instantiate(projectile, firePoint.position, firePoint.rotation);
                    }
                    fireCounter = fireRate;
                }
            }
        }
    }

    void Die()
    {
        manager.enemiesDefeated++;
        manager.enemiesTillDrop--;
        manager.score += scoreYield;
        if(manager.enemiesTillDrop == 0)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
            manager.randomizeDrop();
        }
        if(manager.enemiesDefeated % manager.enemiesTillBoss == 0)
        {
            manager.spawnBoss();
        }
        Instantiate(scoreSprite, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void resetColor()
        {
            render.color = normalColor;
        }

    private void OnCollisionEnter2D(Collision2D other) {
        render.color = redColor;
        Invoke("resetColor", (Time.deltaTime * 3));
    }
}
