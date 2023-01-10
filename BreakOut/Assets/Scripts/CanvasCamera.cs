using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCamera : BaseBehaviour
{
    private Canvas canvas;
    public void Start()
    {
        GetCanvas().worldCamera = Camera.main;
    }

    private Canvas GetCanvas()
    {
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }

        return canvas;
    }
}
