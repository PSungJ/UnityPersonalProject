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
    public AudioClip moveClip;
    public AudioClip dieClip;

    private int spawnCnt = 0;   // 새로 생성되는 계단 카운트

    [SerializeField] private StairSpawn spawn;
    [SerializeField] private MenuControl menu;
    [SerializeField] private UIScript hpBarUI;
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
        if (isDie || menu.isPause) return;
        PcTest();
        zeroHP();
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
        if (isDie || menu.isPause) return;

        if (isFail(false)) // 잘못된 방향으로 가거나 Hp가 0이하가 되면 사망
        {
            Debug.Log("사망");
            Die();
        }
        source.PlayOneShot(moveClip);
        MoveDirection();
        moveCnt++;
        if (moveCnt > 5)
            NewStair();
        
        GameManager.gameInstance.AddScore();
    }

    public void CharTurn()
    {
        if (isDie || menu.isPause) return;
        isTurn = isTurn == true ? false : true;
        sprite.flipX = isTurn;
    }

    public void Die()
    {
        GameManager.gameInstance.GameOver();
        ani.SetTrigger("Dead");
        source.PlayOneShot(dieClip);
        isDie = true;
    }

    public void zeroHP()
    {
        if (UIScript.ui_Instance.curHp == 0)
        {
            Die();
        }
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
    public bool isFail(bool playerTurn)
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
