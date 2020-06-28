using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShotContainer : MonoBehaviour
{
    public float duration;

    private void Awake() {
        Destroy(this.gameObject, duration);
    }
}
