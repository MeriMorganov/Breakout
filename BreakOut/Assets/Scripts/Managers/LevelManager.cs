using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseBehaviour // Loads the right level
{
    private const string BACKGROUND_PREFIX = "Background_";
    public const string LEVEL_PREFIX = "Levels/Level_";
    public const int MIN_LEVELS = 1;
    public const int MAX_LEVELS = 3;
    private int currentLevel = MIN_LEVELS;
    private int numOfBricks = 0;
    private bool gameEnded = false;
    private static LevelManager instance;

    public int NumOfBricks // How many bricks are left in the level
        {
        set{
            numOfBricks = Mathf.Clamp(value, 0, BrickMapper.MAX_BRICKS);
        }
        get { return numOfBricks; }
        }
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("LevelManager");
                instance = go.AddComponent<LevelManager>();
            }
            return instance;
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public void SetLevel(int level)
    {
        currentLevel = level;
        UIManager.Instance.SetLevelValue(currentLevel);
    }

    public void GoToNextLevel()
    {
        SetLevel(++currentLevel);
    }

    public string GetBackgroundPath()
    {
        return $"{BACKGROUND_PREFIX}{GetCurrentLevel()}";
    }

    public string GetLevelPath()
    {
        return $"{LEVEL_PREFIX}{GetCurrentLevel()}";
    }

    public void SetBricksForCurrentLevel()
    {
        BrickMapper.Instance.ConstructLevel(GetCurrentLevel());
    }

    public bool CheckIfLevelFinished()
    {
        if(NumOfBricks <=0)
        {
            if (GetCurrentLevel() == MAX_LEVELS)
            {
                UIManager.Instance.ShowLosePopup(false);
                UIManager.Instance.ShowStartPopup(false);
                UIManager.Instance.ShowWinPopup(true);
                SetLevel(MIN_LEVELS);
            }
            else
            {
                UIManager.Instance.ShowWinPopup(false);
                UIManager.Instance.ShowLosePopup(false);
                UIManager.Instance.ShowStartPopup(true);
                GoToNextLevel();
            }
            GameManager.Instance.InitGame();
            return true;
        }
        return false;
    }

    public bool CheckIfOutOfLives(int currentLives)
    {
        if (currentLives <= 0)
        {
            UIManager.Instance.ShowWinPopup(false);
            UIManager.Instance.ShowStartPopup(false);
            UIManager.Instance.ShowLosePopup(true);
            SetLevel(MIN_LEVELS);

            GameManager.Instance.InitGame();
            return true;
        }
        return false;
    }
}
