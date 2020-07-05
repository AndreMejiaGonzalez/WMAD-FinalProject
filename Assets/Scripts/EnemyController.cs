using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform playerPos;
    public Transform[] firePoints;
    public GameObject projectile;
    public float moveSpeed;
    public float rotationSpeed;
    private float rotationCounter;
    private float rotateSide;
    public bool shouldFire;
    public float fireRate;
    private float fireCounter;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rotateSide = 135;
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
        }
        FireHandler();
    }

    void W_TypeHandler()
    {
        if(transform.position.x - playerPos.position.x > 0 &&
        transform.position.x - playerPos.position.x < 1 &&
        playerPos.position.y < transform.position.y)
        {
            transform.rotation = new Quaternion(0, 0, 1, 0);
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
            transform.rotation = Quaternion.Euler(0, 0, rotateSide);
            rotateSide = rotateSide * -1;
            rotationCounter = rotationSpeed;
        }
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    void FireHandler()
    {
        if(shouldFire)
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
}
