using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    [SerializeField] BoxCollider2D box2D;
    private float height;
    private Transform tr;

    void Awake()
    {
        tr = transform;
        box2D = GetComponent<BoxCollider2D>();
        height = box2D.size.y;
    }

    void Update()
    {
        if (tr.position.y <= -height)
        {
            Vector2 offset = new Vector2(0, height * 2f);
            tr.position = (Vector2)tr.position + offset;
        }
    }
}
