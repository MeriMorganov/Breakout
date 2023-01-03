using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCanvas : BaseBehaviour
{
    private string CANVAS_OBJ_NAME = "Canvas";
    protected SpriteRenderer spriteRenderer;
    protected Canvas canvas;
    public void Start()
    {
        GetCanvas().worldCamera = Camera.main;
    }

    public Canvas GetCanvas()
    {
        if (canvas == null)
        {
            canvas = FindInChild(CANVAS_OBJ_NAME).GetComponent<Canvas>();
        }

        return canvas;
    }
}
