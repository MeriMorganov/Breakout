using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;

public class BrickMapper: BaseBehaviour // Layout the bricks for the level based on a text file
{
    private const char RED_BRICK = 'R';
    private const char ORANGE_BRICK = 'O';
    private const char YELLOW_BRICK = 'Y';
    private const char GREEN_BRICK = 'G';
    private const char BLUE_BRICK = 'B';
    private const char PURPLE_BRICK = 'P';
    private const char WHITE_BRICK = 'W';
    private const char NO_BRICK = '0';
    private const char BLOCK_BRICK = '1';
    public const int MAX_BRICKS = 30;
    private const int NEW_ROW_AT = 5;
    public const float POS_X = -2.16f;//-6.4f;
    public const float POS_Y = 4.78f;//10.34f;
    private const float START_BRICK_X = 0f;
    private const float START_BRICK_Y = 0f;
    private const float OFFSET_BRICK_X = 1.08f;
    private const float OFFSET_BRICK_Y = 0.6f;

    private static BrickMapper instance;
    public static BrickMapper Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("BrickMapper");
                instance = go.AddComponent<BrickMapper>();
                instance.brickPrefab = Resources.Load<GameObject>("Brick");
            }
            return instance;
        }
    }

    private string[] brickMaps = new string[MAX_BRICKS];
    private GameObject[] brickObjects = new GameObject[MAX_BRICKS];
    private Brick[] bricks = new Brick[MAX_BRICKS];
    private GameObject brickPrefab;

    private string LoadFile(string filePath)
    {
        TextAsset text = Resources.Load(filePath) as TextAsset;
        string str = text.ToString().Replace("\n", "").Replace(" ", "").Replace("\r", "");
        return str;
    }

    public void LoadAllLevels(int maxLevels, string levelPrefix)
    {
        for(int i = 0;i < maxLevels; i++)
        {
            brickMaps[i] = LoadFile($"{levelPrefix}{i+1}");
        }
    }
    private GameObject CreateBrick(float x, float y)
    {
        GameObject brick = GameObject.Instantiate(brickPrefab,this.transform);
        brick.transform.position = new Vector3(x, y, 0);
        return brick;
    }
    public void InstantiateBrickPool()
    {
        int row = 0;
        int col = 0;
        for(int i = 0; i < MAX_BRICKS; i++)
        {
            if(i % NEW_ROW_AT == 0)
            {
                row++;
                col = 0;
            }
            brickObjects[i] = CreateBrick(START_BRICK_X + (OFFSET_BRICK_X* col), START_BRICK_Y - (OFFSET_BRICK_Y* row));
            bricks[i] = brickObjects[i].GetComponent<Brick>();
            col++;
        }

        transform.position = new Vector3(POS_X, POS_Y, 0);
    }
    public void ConstructLevel(int level)
    {
        int levelIndex = level - 1;
        int currentBrick = 0;
        LevelManager.Instance.NumOfBricks = 0;
        string currentBrickMap = brickMaps[levelIndex];
        foreach (char brickKey in currentBrickMap)
        {
            if (brickKey == NO_BRICK)
            {
                brickObjects[currentBrick].SetActive(false);
            }
            else
            {
                brickObjects[currentBrick].SetActive(true);
                LevelManager.Instance.NumOfBricks++;
                if (brickKey == BLOCK_BRICK)
                {
                    bricks[currentBrick].ToggleNormalBrick(false);
                    bricks[currentBrick].ToggleBlockBrick(true);
                }
                else
                {
                    bricks[currentBrick].ToggleNormalBrick(true);
                    bricks[currentBrick].ToggleBlockBrick(false);
                    SetBrickColor(currentBrick, brickKey);
                }
            }
            currentBrick++;
        }
    }

    private void SetBrickColor(int index,char brickKey)
    {
        switch (brickKey)
        {
            case RED_BRICK:
                bricks[index].SetBrickColor(Brick.BrickTintColor.Red);
                break;
            case ORANGE_BRICK:
                bricks[index].SetBrickColor(Brick.BrickTintColor.Orange);
                break;
            case YELLOW_BRICK:
                bricks[index].SetBrickColor(Brick.BrickTintColor.Yellow);
                break;
            case GREEN_BRICK:
                bricks[index].SetBrickColor(Brick.BrickTintColor.Green);
                break;
            case BLUE_BRICK:
                bricks[index].SetBrickColor(Brick.BrickTintColor.Blue);
                break;
            case PURPLE_BRICK:
                bricks[index].SetBrickColor(Brick.BrickTintColor.Purple);
                break;
            case WHITE_BRICK:
                bricks[index].SetBrickColor(Brick.BrickTintColor.White);
                break;
        }
    }

}
