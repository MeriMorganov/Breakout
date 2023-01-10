using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GameManager : BaseBehaviour // Manages the whole game
{
    public enum GameMode
    {
        Pause,
        Play
    }
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("GameManager");
                instance = go.GetComponent<GameManager>();
            }
            return instance;
        }
    }
    private GameMode gamemode = GameMode.Pause;

    public const int START_LIVES = 3;
    public const int MAX_LIVES = 99;
    public GameObject backgroundPrefab;
    public GameObject paddlePrefab;
    public GameObject wallColliderPrefab;
    public GameObject uiManagerPrefab;

    private GameObject backgroundObj;
    private GameObject paddleObj;
    private GameObject wallColliderObj;
    public GameObject uiManagerObj;
    public LevelBackground levelBackground;

    public void Start()
    {
        GetBackgroundObj();
        GetLevelBackground();
        SetBackgroundImage();
        InstantiatePrefabObjects();
        BrickMapper.Instance.InstantiateBrickPool();
        BrickMapper.Instance.LoadAllLevels(LevelManager.MAX_LEVELS, LevelManager.LEVEL_PREFIX);
        LevelManager.Instance.SetBricksForCurrentLevel();
        LevelManager.Instance.SetLevel(LevelManager.MIN_LEVELS);
        UIManager.Instance.UpdateStartPopup();
    }

    private void InstantiatePrefabObjects()
    {
        paddleObj = GameObject.Instantiate(paddlePrefab);
        wallColliderObj = GameObject.Instantiate(wallColliderPrefab);
        uiManagerObj = GameObject.Instantiate(uiManagerPrefab);
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

    public void TogglePausePlay()
    {
        gamemode = gamemode == GameMode.Pause ? GameMode.Play : GameMode.Pause;
        UIManager.Instance.UpdateStartPopup();
    }

    public GameMode GetGameMode()
    {
        return gamemode;
    }
}
