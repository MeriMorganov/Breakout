using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBackground : BaseCanvas
{
    private string SPRITERENDERER_OBJ_NAME = "BackgroundImage";



    public SpriteRenderer GetSpriteRenderer()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = FindInChild(SPRITERENDERER_OBJ_NAME).GetComponent<SpriteRenderer>();
        }
        return spriteRenderer;
    }

    public void SetBackgroundSprite(Sprite sprite)
    {
        GetSpriteRenderer().sprite = sprite;
    }

}
