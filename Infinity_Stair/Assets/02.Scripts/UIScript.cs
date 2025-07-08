using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Slider hpSlider; // HpBar Slider UI
    public float maxHP = 100f;
    public float curHp;
    public float hpDrease = 20f; // 초당 감소시킬 Hp
    public float hpRecover = 5f; // Hp 회복량

    void Awake()
    {
        curHp = maxHP;
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;  // Slider 최대값
            hpSlider.value = curHp;     // Slider 현재값
        }
    }

    void Update()
    {
        DecreaseHpBar();
        if (curHp <= 0)
        {
            curHp = 0;      // Hp 0으로 고정
            UpdateHpBar();  // HpBar 업데이트
            GameManager.gameInstance.Die(); // 게임매니저의 Die 호출
        }
    }

    public void RecoverHp()
    {
        curHp += hpRecover;
        curHp = Mathf.Min(maxHP, curHp);    // 현재 Hp가 maxHp를 초과하지않도록 보정
        UpdateHpBar();
    }

    public void DecreaseHpBar()
    {
        curHp -= hpDrease * Time.deltaTime;     // 현재 Hp에서 시간에 비례하여 Hp 감소
        curHp = Mathf.Max(0, curHp);    // 현재 Hp가 0보다 작아지지 않도록 보정
        UpdateHpBar();
    }

    public void UpdateHpBar()  // HpBar UI를 현재 Hp값으로 업데이트
    {
        if (GameManager.gameInstance.isGameOver)
            return;
        else if (hpSlider != null && !GameManager.gameInstance.isGameOver)
            hpSlider.value = curHp;
    }
}
