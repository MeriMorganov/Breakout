using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
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
    private Paddle paddle;
    private GameObject wallColliderObj;
    public GameObject uiManagerObj;
    public LevelBackground levelBackground;

    public void Start()
    {
        GetBackgroundObj();
        GetLevelBackground();
        GetPaddle();
        InstantiatePrefabObjects();
        BrickMapper.Instance.InstantiateBrickPool();
        BrickMapper.Instance.LoadAllLevels(LevelManager.MAX_LEVELS, LevelManager.LEVEL_PREFIX);
        LevelManager.Instance.SetLevel(LevelManager.MIN_LEVELS);
        InitGame();
        UIManager.Instance.UpdateStartPopup();
    }

    public void InitGame()
    {
        SetGameMode(GameManager.GameMode.Pause);
        GetPaddle()?.SetBallToPaddle();
        paddle?.GetBall()?.GetBallPhysics()?.UpdateRBSleep();
        LevelManager.Instance.SetBricksForCurrentLevel();
        SetBackgroundImage();
    }


    private void InstantiatePrefabObjects()
    {
        paddleObj = GameObject.Instantiate(paddlePrefab);
        wallColliderObj = GameObject.Instantiate(wallColliderPrefab);
        uiManagerObj = GameObject.Instantiate(uiManagerPrefab);
    }

    public Paddle GetPaddle()
    {
        if(paddle == null)
        {
            paddle = paddleObj?.GetComponent<Paddle>();
        }
        return paddle;
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
        if (backgroundSprite != null)
        {
            GetLevelBackground().SetBackgroundSprite(backgroundSprite);
        }
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

    public void SetGameMode(GameMode mode)
    {
        gamemode = mode;
    }
}
