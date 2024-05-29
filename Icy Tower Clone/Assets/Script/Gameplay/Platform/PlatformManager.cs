using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{    
    private const int TOTAL_START_PLATFORM = 10;

    [Header("Prefab Platform")]
    [SerializeField] private GameObject regularPlatform;
    [SerializeField] private GameObject smallPlatform;
    [SerializeField] private GameObject bigPlatform;
    [SerializeField] private Transform startPlatform;

    [Header("Level Platform")]
    [SerializeField] private Color[] PlatformColor;
    [SerializeField] private float platFormSpace;
    [SerializeField] private Transform enviromentObject;

    private float startPlatformY;

    private int totalPlatform = 1;
    internal int playerHighestPlatform = 0;

    void Start()
    {
        Init();
    }

    void Init()
    {
        InitPlatform();
    }

    public void CreatePlatform(int _platFormIndex)
    {
        if (_platFormIndex > playerHighestPlatform)
        {
            playerHighestPlatform = _platFormIndex;

            int score = playerHighestPlatform * 100;
            GameManager.Instance.inGameUI.UpdatePlayerScore(score);

            SpawnPlatform();
        }
    }
    void InitPlatform()
    {
        startPlatformY = startPlatform.transform.position.y;

        for (int i = 0; i < TOTAL_START_PLATFORM; i++)
        {
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        GameObject temp = SimplePool.Spawn(regularPlatform, new Vector3(0, startPlatformY + platFormSpace * totalPlatform), Quaternion.identity);
        temp.transform.SetParent(enviromentObject);
        temp.GetComponent<Platform>().platFormIndex = totalPlatform;

        totalPlatform++;        
    }
}
