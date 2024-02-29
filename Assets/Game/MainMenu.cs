using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;
    [SerializeField] private MoveController moveController;
    [SerializeField] private AudioSource buttonPlayer;
    [Space(15)]
    [SerializeField] private CanvasGroup fadeBackground;
    [Space(15)]
    [SerializeField] private RectTransform settingsWindow;
    [SerializeField] private RectTransform allGamesWindow;

    public void btn_Settings()
    {
        buttonPlayer.Play();
        fadeController.Appear(fadeBackground);
        moveController.MoveIn(settingsWindow);
    }

    public void btn_CloseSettings()
    {
        buttonPlayer.Play();
        moveController.MoveOut(settingsWindow);
        fadeController.Disappear(fadeBackground);
    }

    public void btn_AllGames()
    {
        buttonPlayer.Play();
        fadeController.Appear(fadeBackground);
        moveController.MoveIn(allGamesWindow);
    }

    public void btn_CloseAllGames()
    {
        buttonPlayer.Play();
        moveController.MoveOut(allGamesWindow);
        fadeController.Disappear(fadeBackground);
    }
}