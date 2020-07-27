using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSegment : MonoBehaviour
{
    private Manager manager;
    public SFX sfx;
    public BossController controller;
    private SpriteRenderer render;
    public Transform startPos;
    public float hp;
    public int index;
    public bool isHead;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
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
        controller.moveSpeed += 1.5f;
        if(controller.segments.Count == 1)
        {
            controller.enableHead();
        }
        manager.score += 2000;
        if(isHead)
        {
            controller.die();
        }
        Destroy(this.gameObject);
    }

    void resetColor()
    {
        render.color = new Color(255, 255, 255, 1);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(sfx != null)
        {
            sfx.playClip(0);
        }
        render.color = new Color(255, 0, 0, 1);
        Invoke("resetColor", (Time.deltaTime * 3));
    }
}
