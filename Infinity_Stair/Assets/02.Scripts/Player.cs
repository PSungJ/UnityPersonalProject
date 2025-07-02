using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private SpriteRenderer sprite;
    private Vector2 startPos;
    private Vector2 oldPos;
    
    private int moveCnt = 0;
    private int turnCnt = 0;
    private bool isTurn = false;
    private bool isDie = false;

    private int spawnCnt = 0;   // 새로 생성되는 계단 카운트

    [SerializeField] private StairSpawn spawn;
    void Start()
    {
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        oldPos = transform.localPosition;
        isDie = false;
    }

    void Update()
    {
        if (isDie) return;

        if (Input.GetMouseButtonDown(0))
        {
            CharMove();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CharTurn();
        }
        
    }
    private void CharMove()
    {
        MoveDirection();
        moveCnt++;

        if (isFail()) // 잘못된 방향으로 갈 시 사망
        {
            Die();
        }

        if (moveCnt > 5)
            NewStair();
    }
    private void CharTurn()
    {
        isTurn = isTurn == true ? false : true;
        sprite.flipX = isTurn;
    }
    private void Die()
    {
        ani.SetTrigger("Dead");
        isDie = true;
    }

    private void MoveDirection()
    {
        if (isTurn) //Left
        {
            oldPos += new Vector2(-spawn.stairStepX, spawn.stairStepY);
        }
        else
        {
            oldPos += new Vector2(spawn.stairStepX, spawn.stairStepY);
        }
        transform.position = oldPos;
        ani.SetTrigger("Move");
    }
    private bool isFail()
    {
        bool result = false;
        if (spawn.isTurn[turnCnt] != isTurn)
        {
            result = true;
        }
        turnCnt++;

        if (turnCnt > StairsPooling.p_Instance.stairPool.Count - 1)
            turnCnt = 0;

        return result;
    }
    private void NewStair()
    {
        spawn.RespawnStairs(spawnCnt);
        spawnCnt++;

        if (spawnCnt > StairsPooling.p_Instance.stairPool.Count - 1)
            spawnCnt = 0;
    }
}
