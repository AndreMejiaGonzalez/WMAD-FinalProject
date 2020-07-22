using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Manager manager;
    public List<BossSegment> segments;
    public BossSegment head;
    public GameObject lifeDrop;
    public GameObject shieldDrop;
    public GameObject projectile;
    public Transform[] firePoints;
    public Transform[] headFirePoints;
    private Vector3 right;
    private Vector3 left;
    private Vector3 rotateSide;
    public float moveSpeed;
    public bool headIsEnabled;
    public float fireRate;
    private float fireCounter;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        right = transform.right;
        left = -transform.right;
        if(Random.value <= .5f)
        {
            rotateSide = right;
        } else
        {
            rotateSide = left;
        }
        head.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        fireCounter = fireRate;
    }

    void Update()
    {
        movement();
        fireHandler();
    }

    void movement()
    {
        float step = moveSpeed * Time.deltaTime;
        if(transform.position.y != 2.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 2.5f), step);
        } else {
            if(transform.position.x <= -8.8f)
            {
                rotateSide = left;
            } else if(transform.position.x >= 8.8f)
            {
                rotateSide = right;
            }
            transform.position += rotateSide * step;
        }
    }

    void fireHandler()
    {
        if(firePoints.Length > 0)
        {
            fireCounter -= (Time.deltaTime * manager.globalMultiplier);
            if(fireCounter <= 0)
            {
                fireCounter = fireRate;
                foreach(Transform firePoint in firePoints)
                {
                    if(firePoint != null)
                    {
                        Instantiate(projectile, firePoint.position, firePoint.rotation);
                    }
                }
                if(headIsEnabled)
                {
                    foreach(Transform firePoint in headFirePoints)
                    {
                        if(firePoint != null)
                        {
                            Instantiate(projectile, firePoint.position, firePoint.rotation);
                        }
                    }
                }
            }
        }
    }

    public void enableHead()
    {
        head.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        headIsEnabled = true;
    }

    public void die()
    {
        manager._state = Manager.GameState.Active;
        manager.bossSpawned = false;
        Instantiate(lifeDrop, new Vector3((transform.position.x - 2), transform.position.y, 0)
        , Quaternion.identity);
        Instantiate(shieldDrop, new Vector3((transform.position.x + 2), transform.position.y, 0)
        , Quaternion.identity);
        manager.globalMultiplier += .1f;
        Destroy(this.gameObject);
    }
}
