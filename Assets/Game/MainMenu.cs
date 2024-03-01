using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;
    [SerializeField] private MoveController moveController;
    [SerializeField] private AudioSource buttonPlayer;
    [Space(15)]
    [SerializeField] private CanvasGroup smoothTransition;
    [SerializeField] private CanvasGroup fadeBackground;
    [SerializeField] private CanvasGroup gameMenu;
    [SerializeField] private CanvasGroup mainMenu;

    [Space(15)]
    [SerializeField] private RectTransform traillWindow;
    [SerializeField] private RectTransform transformWindow;
    [SerializeField] private RectTransform upgradeWindow;
    [SerializeField] private RectTransform settingsWindow;
    [SerializeField] private RectTransform allGamesWindow;

    public void btn_Settings()
    {
        buttonPlayer.Play();
        CanvasGroup[] groupToAppear = new CanvasGroup[] { fadeBackground };
        fadeController.Appear(groupToAppear);
        moveController.MoveIn(settingsWindow);
    }

    public void btn_CloseSettings()
    {
        buttonPlayer.Play();
        moveController.MoveOut(settingsWindow);
        CanvasGroup[] groupToDesappear = new CanvasGroup[] { fadeBackground };
        fadeController.Disappear(groupToDesappear);
    }

    public void btn_AllGames()
    {
        buttonPlayer.Play();
        CanvasGroup[] groupToAppear = new CanvasGroup[] { fadeBackground };
        fadeController.Appear(groupToAppear);
        moveController.MoveIn(allGamesWindow);
    }

    public void btn_CloseAllGames()
    {
        buttonPlayer.Play();
        moveController.MoveOut(allGamesWindow);
        CanvasGroup[] groupToDesppear = new CanvasGroup[] { fadeBackground };
        fadeController.Disappear(groupToDesppear);
    }

    public void btn_Transport()
    {
        buttonPlayer.Play();
        CanvasGroup[] groupToAppear = new CanvasGroup[] { fadeBackground };
        fadeController.Appear(groupToAppear);
        moveController.MoveIn(transformWindow);
    }

    public void btn_CloseTransport()
    {
        buttonPlayer.Play();
        CanvasGroup[] groupToDesppear = new CanvasGroup[] { fadeBackground };
        fadeController.Disappear(groupToDesppear);
        moveController.MoveOut(transformWindow);
    }

    public void btn_Traill()
    {
        CanvasGroup[] groupToAppear = new CanvasGroup[] { fadeBackground };
        buttonPlayer.Play();
        fadeController.Appear(groupToAppear);
        moveController.MoveIn(traillWindow);
    }

    public void btn_CloseTraill()
    {
        buttonPlayer?.Play();
        moveController.MoveOut(traillWindow);
        CanvasGroup[] groupToDesppear = new CanvasGroup[] { fadeBackground };
        fadeController.Disappear(groupToDesppear);
    }

    public void btn_Play()
    {
        buttonPlayer.Play();
        CanvasGroup[] groupToDisappear = new CanvasGroup[] { mainMenu };
        fadeController.Disappear(groupToDisappear);
        Invoke("LoadDelay", 1f);
    }

    private void LoadDelay()
    {
        SceneManager.LoadScene("Level (1) Countryside");

    }
}