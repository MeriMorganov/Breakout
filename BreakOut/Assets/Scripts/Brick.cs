using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : BaseBehaviour // Row of bricks in the level that need to be cleared out with the ball to finish the level
{
    public enum BrickTintColor
    {
        White = 0,
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Purple
    }

    public GameObject normalBrick;
    public GameObject blockBrick;
    public Color orangeColor = new Color(255, 165, 0);
    public Color purpleColor = new Color(255, 0, 255);

    private SpriteRenderer spriteRenderer = null;

    public SpriteRenderer GetSpriteRenderer()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = normalBrick.GetComponent<SpriteRenderer>();
        }
        return spriteRenderer;
    }

    public void SetBrickColor(BrickTintColor color)
    {
        switch(color)
        {
            case BrickTintColor.Red:
                GetSpriteRenderer().color = Color.red;
                break;
            case BrickTintColor.Orange:
                GetSpriteRenderer().color = orangeColor;
                break;
            case BrickTintColor.Yellow:
                GetSpriteRenderer().color = Color.yellow;
                break;
            case BrickTintColor.Green:
                GetSpriteRenderer().color = Color.green;
                break;
            case BrickTintColor.Blue:
                GetSpriteRenderer().color = Color.blue;
                break;
            case BrickTintColor.Purple:
                GetSpriteRenderer().color = purpleColor;
                break;
        }
    }

    public void ToggleNormalBrick(bool visible)
    {
        normalBrick.SetActive(visible);
    }

    public void ToggleBlockBrick(bool visible)
    {
        blockBrick.SetActive(visible);
    }


}
