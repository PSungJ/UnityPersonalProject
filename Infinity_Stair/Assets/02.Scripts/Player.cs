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

    private AudioSource source;

    private int spawnCnt = 0;   // ���� �����Ǵ� ��� ī��Ʈ

    [SerializeField] private StairSpawn spawn;
    void Start()
    {
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        startPos = transform.position;
        oldPos = transform.localPosition;
        isDie = false;
    }

    void Update()
    {
        if (isDie) return;
        PcTest();
    }

    private void PcTest()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CharMove();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CharTurn();
        }
    }

    public void CharMove()
    {
        if (isDie) return;
        source.Play();
        MoveDirection();
        moveCnt++;

        if (isFail()) // �߸��� �������� �� �� ���
        {
            Die();
        }

        if (moveCnt > 5)
            NewStair();
        GameManager.gameInstance.AddScore();
    }
    public void CharTurn()
    {
        if (isDie) return;
        isTurn = isTurn == true ? false : true;
        sprite.flipX = isTurn;
    }
    private void Die()
    {
        GameManager.gameInstance.GameOver();
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
