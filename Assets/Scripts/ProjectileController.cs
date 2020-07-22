using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Manager manager;
    public GameObject explode;
    public float shotSpeed;
    public float damage;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
    }

    void Update()
    {
        transform.position += this.transform.up * shotSpeed * Time.deltaTime;
    }

    void DoDamage(EnemyController enemy)
    {
        enemy.hp -= damage;
    }

    void DoDamage(BossSegment segment)
    {
        segment.hp -= (damage / manager.globalMultiplier);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(this.gameObject.tag == "BombShot" && other.gameObject.tag != "Kill Plane")
        {
            Instantiate(explode, transform.position, transform.rotation);
        }
        if(this.gameObject.tag != "WaveShot")
        {
            Destroy(this.gameObject);
        }
        if(this.gameObject.tag != "BombShot" && other.gameObject.layer == 14)
        {
            if(other.gameObject.tag == "Boss")
            {
                DoDamage(other.gameObject.GetComponent<BossSegment>());
            } else {
                DoDamage(other.gameObject.GetComponent<EnemyController>());
            }
        }
    }
}
