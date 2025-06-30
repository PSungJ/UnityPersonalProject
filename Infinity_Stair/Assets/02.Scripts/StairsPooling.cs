using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsPooling : MonoBehaviour
{
    public static StairsPooling p_Instance {  get; private set; }

    [Header("Stairs Pool Setting")]
    public GameObject stairPrefabs;   // 계단 프리팹
    public int poolSize = 20;           // 미리 생성할 오브젝트 갯수
    public List<GameObject> stairPool = new List<GameObject>();

    // 풀에 보관될 오브젝트들을 담는 딕셔너리
    // Key: 프리팹의 해시 코드 또는 이름 (여기서는 프리팹 자체를 키로 사용)
    // Value: 해당 프리팹으로 생성된, 현재 비활성화된(사용 가능한) 오브젝트들의 리스트
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
