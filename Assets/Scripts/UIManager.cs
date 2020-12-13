using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text text;
    public Button button;

    public GameController gameContr;

    public void Start()
    {
        button.onClick.AddListener(gameContr.Reset);
        button.onClick.AddListener(Reset);
    }
    public void StepsUpdate()
    {
        text.text = " Steps: " + Player.playerSteps;
    }
    internal void Reset()
    {
        StepsUpdate();
    }

}
