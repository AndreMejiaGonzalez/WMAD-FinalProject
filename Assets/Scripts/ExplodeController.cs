using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{
    PlayerController player;
    public float damage;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.canFire = false;
    }

    void DoDamage(EnemyController enemy)
    {
        enemy.hp -= damage;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.layer == 14)
        {
            DoDamage(other.gameObject.GetComponent<EnemyController>());
        }
    }

    private void OnDestroy() {
        player.canFire = true;
    }
}
