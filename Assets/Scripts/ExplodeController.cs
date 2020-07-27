using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{
    private Manager manager;
    public SFX sfx;
    public float damage;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        sfx.playClip(0);
    }

    void DoDamage(EnemyController enemy)
    {
        enemy.hp -= damage;
    }

    void DoDamage(BossSegment segment)
    {
        if(damage / manager.globalMultiplier < 1)
        {
            segment.hp -= 1;
        } else {
            segment.hp -= (damage / manager.globalMultiplier);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.layer == 14)
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
