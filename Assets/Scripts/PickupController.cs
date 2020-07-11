using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public GameObject weapon;
    public float fireRate;
    public float speed;

    private void Update() {
        transform.position += -transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if(this.gameObject.tag == "WeaponPickup")
            {
                player.projectile = weapon;
                player.fireRate = fireRate;
            } else if(this.gameObject.tag == "LifePickup")
            {
                player.lives++;
            } else if(this.gameObject.tag == "ShieldPickup")
            {
                player.shield.SetActive(true);
            }
        Destroy(this.gameObject);
        }
    }
}
