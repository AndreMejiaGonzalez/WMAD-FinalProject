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
    private Vector3 side;
    public float moveSpeed;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        side = transform.right;
        head.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        movement();
    }

    void turn()
    {
        side = side * -1;
    }

    void movement()
    {
        float step = moveSpeed * Time.deltaTime;
        if(transform.position.y != 2.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 2.5f), step);
        } else {
            if(transform.position.x <= -8.8f || transform.position.x >= 8.8f)
            {
                turn();
            }
            transform.position += side * step;
        }
    }

    public void enableHead()
    {
        head.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void die()
    {
        Instantiate(lifeDrop, new Vector3((transform.position.x - 2), transform.position.y, 0)
        , Quaternion.identity);
        Instantiate(shieldDrop, new Vector3((transform.position.x + 2), transform.position.y, 0)
        , Quaternion.identity);
        manager._state = Manager.GameState.Active;
        manager.bossSpawned = false;
        Destroy(this.gameObject);
    }
}
