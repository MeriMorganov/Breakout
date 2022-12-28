using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBackground : BaseBehaviour
{
    private string CANVAS_OBJ_NAME = "Canvas";
    private string SPRITERENDERER_OBJ_NAME = "BackgroundImage";
    private SpriteRenderer spriteRenderer;
    private Canvas canvas;

    public void Start()
    {
        GetCanvas().worldCamera = Camera.main;
    }

    public Canvas GetCanvas()
    {
        if(canvas == null)
        {
            canvas = FindInChild(CANVAS_OBJ_NAME).GetComponent<Canvas>();
        }

        return canvas;
    }

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
