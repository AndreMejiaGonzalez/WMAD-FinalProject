using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShotContainer : MonoBehaviour
{
    void Update()
    {
        Destroy(this.gameObject, 3);
    }
}
