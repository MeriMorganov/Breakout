using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseCanvas // Toggles and shows UI components for the game
{
    public TextMesh livesValue; // Shows the lives counter
    public TextMesh levelValue; // Shows the current level
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("UI");
                if (go == null)
                {
                    go = GameObject.Find("UI(Clone)");
                }
                instance = go.GetComponent<UIManager>();
            }
            return instance;
        }

    }

    public void SetLivesValue(int value)
    {
        livesValue.text = value.ToString();
    }

    public void SetLevelValue(int value)
    {
        levelValue.text = value.ToString();
    }
}
