using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseBehaviour // Manages the whole game
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("GameManager");
            }
            return instance;
        }
    }
    public GameObject backgroundPrefab;
    public GameObject paddlePrefab;
    public GameObject wallColliderPrefab;

    private GameObject backgroundObj;
    private GameObject paddleObj;
    private GameObject wallColliderObj;
    public LevelBackground levelBackground;

    public void Start()
    {
        GetBackgroundObj();
        GetLevelBackground();
        SetBackgroundImage();
        BrickMapper.Instance.InstantiateBrickPool();
        BrickMapper.Instance.LoadAllLevels(LevelManager.MAX_LEVELS, LevelManager.LEVEL_PREFIX);
        paddleObj = GameObject.Instantiate(paddlePrefab);
        wallColliderObj = GameObject.Instantiate(wallColliderPrefab);
        LevelManager.Instance.SetBricksForCurrentLevel();
    }

    public LevelBackground GetLevelBackground()
    {
        if(levelBackground == null)
        {
            levelBackground = GetBackgroundObj().GetComponent<LevelBackground>();
        }
        return levelBackground;
    }

    public GameObject GetBackgroundObj()
    {
        if(backgroundObj == null)
        {
            backgroundObj = GameObject.Instantiate(backgroundPrefab);
        }
        return backgroundObj;
    }
    public void SetBackgroundImage()
    {
        Sprite backgroundSprite = Resources.Load<Sprite>(LevelManager.Instance.GetBackgroundPath());
        GetLevelBackground().SetBackgroundSprite(backgroundSprite);
    }
}
