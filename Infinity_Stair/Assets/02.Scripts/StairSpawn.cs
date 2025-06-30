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
    void Start()
    {
        Init();
        SpawnStair();
    }
    private void Init()
    {
        state = State.START;
        oldPos = Vector3.zero;
    }
    private void SpawnStair()
    {
        for (int i = 0; i < StairsPooling.p_Instance.stairPool.Count; i++)
        {
            switch (state)
            {
                case State.START:
                    StairsPooling.p_Instance.stairPool[i].transform.position = new Vector2(0.75f, -0.1f);
                    state = State.RIGHT;
                    break;
                case State.LEFT:
                    StairsPooling.p_Instance.stairPool[i].transform.position = oldPos + new Vector2(-0.75f, 0.5f);
                    break;
                case State.RIGHT:
                    StairsPooling.p_Instance.stairPool[i].transform.position = oldPos + new Vector2(0.75f, 0.5f);
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
}
