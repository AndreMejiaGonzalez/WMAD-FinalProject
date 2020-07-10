using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D other) {
        Destroy(other.gameObject);
    }
}
