using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] SpriteRenderer sprite;
    private Vector2 startPos;
    private Vector2 oldPos;
    private bool isTurn = false;
    void Start()
    {
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        oldPos = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CharTurn();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            CharMove();
        }
    }
    private void CharTurn()
    {
        isTurn = isTurn == true ? false : true;
        sprite.flipX = isTurn;
    }
    private void CharMove()
    {
        if (isTurn)
        {
            oldPos += new Vector2(-0.75f, 0.5f);
        }
        else
        {
            oldPos += new Vector2(0.75f, 0.5f);
        }
        transform.position = oldPos;
        ani.SetTrigger("Move");
    }
}
