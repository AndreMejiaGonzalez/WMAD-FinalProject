using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSegment : MonoBehaviour
{
    public BossController controller;
    private SpriteRenderer render;
    public Transform startPos;
    public float hp;
    public int index;
    public bool isHead;

    private void Awake() {
        render = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if(hp <= 0)
        {
            die();
        }
    }

    void die()
    {
        int indexToRemove = 0;
        for(int i = 0; i < controller.segments.Count; i++)
        {
            if(controller.segments[i].index == index)
            {
                indexToRemove = i;
            }
        }
        controller.segments.RemoveAt(indexToRemove);
        controller.moveSpeed += 5;
        if(controller.segments.Count == 1)
        {
            controller.enableHead();
        }
        Destroy(this.gameObject);
    }

    void resetColor()
    {
        render.color = new Color(255, 255, 255, 1);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        render.color = new Color(255, 0, 0, 1);
        Invoke("resetColor", (Time.deltaTime * 3));
    }

    private void OnDestroy() {
        if(isHead)
        {
            controller.die();
        }
    }
}
