using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsPooling : MonoBehaviour
{
    public static StairsPooling p_Instance {  get; private set; }

    [Header("Stairs Pool Setting")]
    public GameObject stairPrefabs;   // ��� ������
    public int poolSize = 20;           // �̸� ������ ������Ʈ ����
    public List<GameObject> stairPool = new List<GameObject>();

    // Ǯ�� ������ ������Ʈ���� ��� ��ųʸ�
    // Key: �������� �ؽ� �ڵ� �Ǵ� �̸� (���⼭�� ������ ��ü�� Ű�� ���)
    // Value: �ش� ���������� ������, ���� ��Ȱ��ȭ��(��� ������) ������Ʈ���� ����Ʈ
    private Dictionary<GameObject, List<GameObject>> objectPool = new Dictionary<GameObject, List<GameObject>>();
    void Awake()
    {
        if (p_Instance != null && p_Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            p_Instance = this;
        }
        CreateStairs();
    }
    private void CreateStairs()
    {
        GameObject objectPool = new GameObject("StairGroup");
        for (int i = 0; i < poolSize; i++)
        {
            var stair = Instantiate(stairPrefabs, objectPool.transform);
            stair.name = $"Stair {i + 1}";
            stair.SetActive(true);
            stairPool.Add(stair);
        }
    }
    public GameObject GetStairs()
    {
        for (int i = 0; i < stairPool.Count; i++)
        {
            if (stairPool[i].activeSelf == false)
            {
                return stairPool[i];
            }
        }
        return null;
    }
}
