using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Slider hpSlider; // HpBar Slider UI
    public float maxHP = 100f;
    public float curHp;
    public float hpDrease = 20f; // �ʴ� ���ҽ�ų Hp
    public float hpRecover = 5f; // Hp ȸ����

    void Awake()
    {
        curHp = maxHP;
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;  // Slider �ִ밪
            hpSlider.value = curHp;     // Slider ���簪
        }
    }

    void Update()
    {
        DecreaseHpBar();
        if (curHp <= 0)
        {
            curHp = 0;      // Hp 0���� ����
            UpdateHpBar();  // HpBar ������Ʈ
            GameManager.gameInstance.Die(); // ���ӸŴ����� Die ȣ��
        }
    }

    public void RecoverHp()
    {
        curHp += hpRecover;
        curHp = Mathf.Min(maxHP, curHp);    // ���� Hp�� maxHp�� �ʰ������ʵ��� ����
        UpdateHpBar();
    }

    public void DecreaseHpBar()
    {
        curHp -= hpDrease * Time.deltaTime;     // ���� Hp���� �ð��� ����Ͽ� Hp ����
        curHp = Mathf.Max(0, curHp);    // ���� Hp�� 0���� �۾����� �ʵ��� ����
        UpdateHpBar();
    }

    public void UpdateHpBar()  // HpBar UI�� ���� Hp������ ������Ʈ
    {
        if (GameManager.gameInstance.isGameOver)
            return;
        else if (hpSlider != null && !GameManager.gameInstance.isGameOver)
            hpSlider.value = curHp;
    }
}
