using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Manager manager;
    private Transform playerPos;
    public HUDController HUD;
    public List<BossSegment> segments;
    public BossSegment head;
    public BossSegment[] claws;
    public GameObject lifeDrop;
    public GameObject shieldDrop;
    public GameObject projectile;
    public Transform[] firePoints;
    public Transform[] headFirePoints;
    private Vector3 right;
    private Vector3 left;
    private Vector3 rotateSide;
    public float normalizedHP;
    public float moveSpeed;
    public bool headIsEnabled;
    public float fireRate;
    private float fireCounter;
    public float clawAttackRate;
    private float clawAttackCounter;
    private bool clawAttackInProgress;
    private bool clawReachedTarget;
    private Vector3 clawAttackTarget;
    private int clawToMove;
    public float multiplierAddition;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        HUD = GameObject.Find("GameManager").GetComponent<HUDController>();
        HUD.boss = this;
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
        clawAttackCounter = clawAttackRate;
    }

    void Update()
    {
        movement();
        fireHandler();
        setClawAttackVariables();
        clawMovement();
        getNormalizedHP();
    }

    void getNormalizedHP()
    {
        float tmp = 0;
        foreach(BossSegment segment in segments)
        {
            if(segment != null)
            {
                tmp += segment.hp;
            }
        }
        normalizedHP = tmp / 3000;
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
            fireCounter -= Time.deltaTime;
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

    void setClawAttackVariables()
    {
        if(!clawAttackInProgress && (claws[0] != null || claws[1] != null))
        {
            clawAttackCounter -= Time.deltaTime;
            if(clawAttackCounter <= 0)
            {
                clawAttackTarget = playerPos.position;
                if(Random.value <= .5f)
                {
                    if(claws[0] != null)
                    {
                        clawToMove = 0;
                    } else if (claws[1] != null)
                    {
                        clawToMove = 1;
                    }
                } else {
                    if(claws[1] != null)
                    {
                        clawToMove = 1;
                    } else if(claws[0] != null)
                    {
                        clawToMove = 0;
                    }
                }
                clawAttackInProgress = true;
                clawAttackCounter = clawAttackRate;
            }
        }
    }

    void clawMovement()
    {
        float step = moveSpeed * Time.deltaTime;
        if(clawAttackInProgress)
        {
            if(claws[clawToMove] == null)
            {
                clawAttackInProgress = false;
                clawReachedTarget = false;
                return;
            }

            if(!clawReachedTarget)
            {
                claws[clawToMove].transform.position =
                Vector3.MoveTowards(claws[clawToMove].transform.position, clawAttackTarget, step);
                if(claws[clawToMove].transform.position == clawAttackTarget)
                {
                    clawReachedTarget = true;
                }
            } else
            {
                claws[clawToMove].transform.position =
                Vector3.MoveTowards(claws[clawToMove].transform.position, claws[clawToMove].startPos.position
                , step);
                if(claws[clawToMove].transform.position == claws[clawToMove].startPos.position)
                {
                    clawAttackInProgress = false;
                    clawReachedTarget = false;
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
        manager.bossSpawned = false;
        HUD.setBossBarState(false);
        Instantiate(lifeDrop, new Vector3((transform.position.x - 2), transform.position.y, 0)
        , Quaternion.identity);
        Instantiate(shieldDrop, new Vector3((transform.position.x + 2), transform.position.y, 0)
        , Quaternion.identity);
        manager.globalMultiplier += multiplierAddition;
        manager.callStartGame(4);
        Destroy(this.gameObject);
    }
}
