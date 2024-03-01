using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;
    [SerializeField] private MoveController moveController;
    [Space(15)]
    [SerializeField] private CanvasGroup smoothTransitionPanel;
    [SerializeField] private CanvasGroup fadeBackgroundPanel;
    [SerializeField] private CanvasGroup gameMenu;
    [SerializeField] private CanvasGroup mainMenu;
    [Space(15)]
    [SerializeField] private RectTransform traillSelectionWindow;
    [SerializeField] private RectTransform vehicleSelectionWindow;
    [SerializeField] private RectTransform upgradeWindow;
    [SerializeField] private RectTransform settingsWindow;
    [SerializeField] private RectTransform allGamesWindow;

    [SerializeField] private AudioSource buttonSoundsPlayerAudioSource;
    [SerializeField] private AudioSource musicPlayerAudioSource;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image musicButtonImage;
    [Space(15)]
    [SerializeField] private Sprite toggleON;
    [SerializeField] private Sprite toggleOFF;


    public void UpdateUI()
    {
        LoadSoundsSettings();
        LoadMusicSettings();
    }

    private void LoadSoundsSettings()
    {
        bool sounds = YandexGame.savesData.sounds;

        if (sounds == true)
        {
            buttonSoundsPlayerAudioSource.volume = 1f;
            soundsButtonImage.sprite = toggleON;
        }
        else if (sounds == false)
        {
            buttonSoundsPlayerAudioSource.volume = 0f;
            soundsButtonImage.sprite = toggleOFF;
        }
    }

    private void LoadMusicSettings()
    {
        bool music = YandexGame.savesData.music;

        if (music == true)
        {
            musicButtonImage.sprite = toggleON;
            musicPlayerAudioSource.volume = 1f;
        }
        else if (music == false)
        {
            musicPlayerAudioSource.Stop();
            musicPlayerAudioSource.volume = 0f;
            musicButtonImage.sprite = toggleOFF;
        }
    }

    public void btnSounds()
    {
        bool sounds = YandexGame.savesData.sounds;

        if (sounds == true)
        {
            buttonSoundsPlayerAudioSource.volume = 0f;
            soundsButtonImage.sprite = toggleOFF;
            YandexGame.savesData.sounds = false;
        }
        else if (sounds == false)
        {
            buttonSoundsPlayerAudioSource.volume = 1f;
            buttonSoundsPlayerAudioSource.Play();
            soundsButtonImage.sprite = toggleON;
            YandexGame.savesData.sounds = true;
        }

        YandexGame.SaveProgress();
    }

    public void btnMusic()
    {
        buttonSoundsPlayerAudioSource.Play();
        bool music = YandexGame.savesData.music;

        if (music == true)
        {
            musicPlayerAudioSource.Pause();
            musicButtonImage.sprite = toggleOFF;
            YandexGame.savesData.music = false;
        }
        else if (music == false)
        {
            musicPlayerAudioSource.Play();
            musicButtonImage.sprite = toggleON;
            YandexGame.savesData.music = true;
        }

        YandexGame.SaveProgress();
    }


    public void btn_Settings()
    {
        buttonSoundsPlayerAudioSource.Play();
        CanvasGroup[] groupToAppear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Appear(groupToAppear);
        moveController.MoveIn(settingsWindow);
    }

    public void btn_CloseSettings()
    {
        buttonSoundsPlayerAudioSource.Play();
        moveController.MoveOut(settingsWindow);
        CanvasGroup[] groupToDesappear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Disappear(groupToDesappear);
    }

    public void btn_AllGames()
    {
        buttonSoundsPlayerAudioSource.Play();
        CanvasGroup[] groupToAppear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Appear(groupToAppear);
        moveController.MoveIn(allGamesWindow);
    }

    public void btn_CloseAllGames()
    {
        buttonSoundsPlayerAudioSource.Play();
        moveController.MoveOut(allGamesWindow);
        CanvasGroup[] groupToDesppear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Disappear(groupToDesppear);
    }

    public void btn_Transport()
    {
        buttonSoundsPlayerAudioSource.Play();
        CanvasGroup[] groupToAppear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Appear(groupToAppear);
        moveController.MoveIn(vehicleSelectionWindow);
    }

    public void btn_CloseTransport()
    {
        buttonSoundsPlayerAudioSource.Play();
        CanvasGroup[] groupToDesppear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Disappear(groupToDesppear);
        moveController.MoveOut(vehicleSelectionWindow);
    }

    public void btn_Traill()
    {
        CanvasGroup[] groupToAppear = new CanvasGroup[] { fadeBackgroundPanel };
        buttonSoundsPlayerAudioSource.Play();
        fadeController.Appear(groupToAppear);
        moveController.MoveIn(traillSelectionWindow);
    }

    public void btn_CloseTraill()
    {
        buttonSoundsPlayerAudioSource?.Play();
        moveController.MoveOut(traillSelectionWindow);
        CanvasGroup[] groupToDesppear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Disappear(groupToDesppear);
    }

    public void btn_Play()
    {
        buttonSoundsPlayerAudioSource.Play();
        CanvasGroup[] groupToDisappear = new CanvasGroup[] { mainMenu };
        fadeController.Disappear(groupToDisappear);

        CanvasGroup[] groupToAppear = new CanvasGroup[] {smoothTransitionPanel };
        fadeController.Appear(groupToAppear);
        Invoke("LoadDelay", 1f);
    }

    private void LoadDelay()
    {
        SceneManager.LoadScene("Level (1) Countryside");
    }
}