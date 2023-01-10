using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseBehaviour // Toggles and shows UI components for the game
{
    public Text livesValue; // Shows the lives counter
    public Text levelValue; // Shows the current level
    public Text StartPopupLevelValue;
    public GameObject background;
    public GameObject startPopup;
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
        StartPopupLevelValue.text = value.ToString();
    }

    public void UpdateStartPopup()
    {

        if(GameManager.Instance.GetGameMode() == GameManager.GameMode.Pause)
        {
            ShowStartPopup(true);
        }
        else
        {
            ShowStartPopup(false);
        }
    }

    public void ShowStartPopup(bool show)
    {
        background.SetActive(show);
        startPopup.SetActive(show);
    }

}
