using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject explode;
    public float shotSpeed;
    public float damage;

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
        segment.hp -= damage;
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
