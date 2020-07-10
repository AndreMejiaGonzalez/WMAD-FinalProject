using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform playerPos;
    public Transform[] firePoints;
    public GameObject projectile;
    public float hp;
    public float moveSpeed;
    public float rotationSpeed;
    private float rotationCounter;
    private Vector2 rotateSide;
    public float fireRate;
    private float fireCounter;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rotationCounter = rotationSpeed;
        rotateSide = Vector2.right;
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
            rotateSide = rotateSide * -1;
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
            if(transform.position.x >= 10 || transform.position.x <= -10)
            {
                rotateSide = rotateSide * -1;
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
                moveSpeed = 25;
            } else {
                transform.position += transform.up * moveSpeed * Time.deltaTime;
            }
        }
    }

    void FireHandler()
    {
        if(firePoints.Length > 0)
        {
            fireCounter -= Time.deltaTime;
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

    void Die()
    {
        Destroy(this.gameObject);
    }
}
