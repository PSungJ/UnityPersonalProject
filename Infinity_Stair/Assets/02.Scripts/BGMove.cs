using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    private float height;
    private Transform thisPos;
    public Transform otherPos;
    public Transform playerTr;
    private BoxCollider2D box2D;

    void Awake()
    {
        box2D = GetComponent<BoxCollider2D>();
        height = box2D.size.y;
        thisPos = transform;
    }

    void Update()
    {
        if (playerTr.position.y > thisPos.position.y + height)
        {
            Vector2 newPosition = new Vector2(thisPos.position.x, otherPos.position.y + height);
            thisPos.position = newPosition;
        }
        //if (thisPos.position.y >= playerTr.position.y + height)
        //    return;
        //if (playerTr.position.y >= lastPlayerYPos + moveDistance)
        //{
        //    Vector2 offset = new Vector2(0, height * 2f);
        //    thisPos.position = (Vector2)thisPos.position + offset;
        //    lastPlayerYPos = playerTr.position.y;
        //}
    }
}
