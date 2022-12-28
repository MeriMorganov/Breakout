using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseBehaviour // Loads the right level
{
    private const string BACKGROUND_PREFIX = "Background_";
    public const string LEVEL_PREFIX = "Levels/Level_";
    public const int MIN_LEVELS = 1;
    public const int MAX_LEVELS = 1;
    private int currentLevel = MIN_LEVELS;
    private int numOfBricks = 0;
    private static LevelManager instance;

    public int NumOfBricks
        {
        set{
            if (value < 0)
            {
                numOfBricks = 0;
            }
            else if (value > BrickMapper.MAX_BRICKS)
            {
                numOfBricks = BrickMapper.MAX_BRICKS;
            }
            else
            {
                numOfBricks = value;
            }
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
        currentLevel = 0;
    }

    public void GoToNextLevel()
    {
        SetLevel(currentLevel++);
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
}
