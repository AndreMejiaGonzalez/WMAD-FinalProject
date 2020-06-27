using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{
    PlayerController player;
    public float duration;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.canFire = false;
        Destroy(this.gameObject, duration);
    }

    private void OnDestroy() {
        player.canFire = true;
    }
}
