using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairSpawn : MonoBehaviour
{
    public Transform playerTr;

    [Header("Spawn Setting")]
    public float stairStepY = 0.5f;
    public float stairStepX = 0.75f;

    private enum State { START, LEFT, RIGHT };
    private State state;
    private Vector2 oldPos;
    
    public bool[] isTurn;
    public void Start()
    {
        Init();
        SpawnStair();
    }
    public void Init()
    {
        state = State.START;
        oldPos = Vector3.zero;

        isTurn = new bool[StairsPooling.p_Instance.stairPool.Count];

        for (int i = 0; i < StairsPooling.p_Instance.stairPool.Count; i++)
        {
            StairsPooling.p_Instance.stairPool[i].transform.position = Vector3.zero;
            isTurn[i] = false;
        }
    }
    public void SpawnStair()
    {
        for (int i = 0; i < StairsPooling.p_Instance.stairPool.Count; i++)
        {
            switch (state)
            {
                case State.START:
                    StairsPooling.p_Instance.stairPool[i].transform.position = new Vector2(stairStepX, -0.1f);
                    state = State.RIGHT;
                    break;
                case State.LEFT:
                    StairsPooling.p_Instance.stairPool[i].transform.position = oldPos + new Vector2(-stairStepX, stairStepY);
                    isTurn[i] = true;
                    break;
                case State.RIGHT:
                    StairsPooling.p_Instance.stairPool[i].transform.position = oldPos + new Vector2(stairStepX, stairStepY);
                    isTurn[i] = false;
                    break;
            }
            oldPos = StairsPooling.p_Instance.stairPool[i].transform.position;

            if (i != 0)
            {
                int ran = Random.Range(0, 5);

                if (ran < 2 && i< StairsPooling.p_Instance.stairPool.Count - 1)
                {
                    state = state == State.LEFT ? State.RIGHT : State.LEFT;
                }
            }
        }
    }
    public void RespawnStairs(int cnt)
    {
        int ran = Random.Range(0, 5);

        if (ran < 2)
        {
            state = state == State.LEFT ? State.RIGHT : State.LEFT;
        }

        switch (state)
        {
            case State.LEFT:
                StairsPooling.p_Instance.stairPool[cnt].transform.position = oldPos + new Vector2(-stairStepX, stairStepY);
                isTurn[cnt] = true;
                break;
            case State.RIGHT:
                StairsPooling.p_Instance.stairPool[cnt].transform.position = oldPos + new Vector2(stairStepX, stairStepY);
                isTurn[cnt] = false;
                break;
        }
        oldPos = StairsPooling.p_Instance.stairPool[cnt].transform.position;
    }
}
