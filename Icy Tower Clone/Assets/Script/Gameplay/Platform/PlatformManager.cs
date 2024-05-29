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

    [Header("Prefab Wall")]
    [SerializeField] private GameObject wall;
    [SerializeField] private Transform startWall;
    [SerializeField] private float wallSpace;

    [Header("Level Platform")]
    [SerializeField] private Color[] PlatformColor;
    [SerializeField] private float platFormSpace;
    [SerializeField] private Transform enviromentObject;
    [SerializeField] private int levelIncreasePerPlatform;

    private int currentLevel = 0;

    private float startPlatformY;
    private float platformRangeX = 3.4f;
    private float regularPlatWidth;
    private float smallPlatWidth;

    private int totalPlatform = 1;
    internal int playerHighestPlatform = 0;
    internal int currentPlatformDisapear = 0;

    private float starWallY;
    private int totalWall = 1;

    void Start()
    {
        Init();
    }

    void Init()
    {
        InitPlatform();
        InitWall();
    }

    void InitPlatform()
    {
        regularPlatWidth = regularPlatform.GetComponent<SpriteRenderer>().size.x;
        smallPlatWidth = smallPlatform.GetComponent<SpriteRenderer>().size.x;

        startPlatformY = startPlatform.transform.position.y;

        for (int i = 0; i < TOTAL_START_PLATFORM; i++)
        {
            SpawnPlatform();
        }
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
    private void SpawnPlatform()
    {
        GameObject temp; 

        if (totalPlatform % levelIncreasePerPlatform == 0)
        {
            currentLevel++;
            temp = SimplePool.Spawn(bigPlatform, new Vector3(0, startPlatformY + platFormSpace * totalPlatform), Quaternion.identity);
            GameManager.Instance.cameraController.IncreaseScreenSpeed();
        }
        else
        {
            float randomX = 0;

            if(Random.Range(0,2) == 0)
            {
                randomX = Random.Range(-platformRangeX + regularPlatWidth, platformRangeX - regularPlatWidth);
                temp = SimplePool.Spawn(regularPlatform, new Vector3(randomX, startPlatformY + platFormSpace * totalPlatform), Quaternion.identity);
            }
            else
            {
                randomX = Random.Range(-platformRangeX + smallPlatWidth, platformRangeX - smallPlatWidth);
                temp = SimplePool.Spawn(smallPlatform, new Vector3(randomX, startPlatformY + platFormSpace * totalPlatform), Quaternion.identity);
            }            
        }       

        temp.transform.SetParent(enviromentObject);
        temp.GetComponent<Platform>().platFormIndex = totalPlatform;

        if(currentLevel > 0)
        {
            temp.GetComponent<SpriteRenderer>().color = PlatformColor[currentLevel - 1];
        }

        totalPlatform++;        
    }

    void InitWall()
    {
        starWallY = startWall.transform.position.y;

        for(int i = 0; i < 2; i++)
        {
            SpawnWall();
        }  
    }
    public void SpawnWall()
    {
        GameObject temp = SimplePool.Spawn(wall, new Vector3(0, starWallY + wallSpace * totalWall), Quaternion.identity);
        temp.transform.SetParent(enviromentObject);        

        totalWall++;
    }
}
