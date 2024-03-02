using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;
    [SerializeField] private MoveController moveController;
    [Space(15)]
    [SerializeField] private CanvasGroup smoothTransitionPanel;
    [SerializeField] private CanvasGroup fadeBackgroundPanel;
    [Space(15)]
    [SerializeField] private AudioSource buttonSoundsPlayerAudioSource;
    [SerializeField] private RectTransform pauseWindow;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image musicButtonImage;
    [Space(15)]
    [SerializeField] private Sprite toggleON;
    [SerializeField] private Sprite toggleOFF;


    public void Start()
    {
        LoadSoundsSettings();
        LoadMusicSettings();

        CanvasGroup[] disapearGroup = new CanvasGroup[] { smoothTransitionPanel };
        fadeController.Disappear(disapearGroup);
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

        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource musicPlayerAudioSource = musicPlayer.GetComponent<AudioSource>();

        if (music == true)
        {
            musicPlayerAudioSource.volume = 1f;
            musicButtonImage.sprite = toggleON;
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

        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource musicPlayerAudioSource = musicPlayer.GetComponent<AudioSource>();

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


    public void btn_Pause()
    {
        buttonSoundsPlayerAudioSource.Play();

        CanvasGroup[] groupToAppear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Appear(groupToAppear);
        moveController.MoveIn(pauseWindow);
    }

    public void btn_Resume()
    {
        buttonSoundsPlayerAudioSource.Play();

        CanvasGroup[] groupToDisappear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Disappear(groupToDisappear);
        moveController.MoveOut(pauseWindow);
    }

    public void btn_Home()
    {
        buttonSoundsPlayerAudioSource.Play();

        CanvasGroup[] appearGroup = new CanvasGroup[] { smoothTransitionPanel };
        fadeController.Appear(appearGroup);
        moveController.MoveOut(pauseWindow);
        StartCoroutine(LoadDelay("MainMenu"));
    }


    private IEnumerator LoadDelay(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}