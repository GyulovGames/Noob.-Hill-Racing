using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private AudioSource buttonsAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [Space(15)]
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image musicButtonImage;
    [Space(15)]
    [SerializeField] private Sprite toggleON;
    [SerializeField] private Sprite toggleOFF;



    private void Start()
    {
        LoadSoundsSettings();
        LoadMusicSettings();
    }


    private void LoadSoundsSettings()
    {
        bool sounds = YandexGame.savesData.sounds;

        if (sounds == true)
        {
            buttonsAudioSource.volume = 1f;
            soundsButtonImage.sprite = toggleON;
        }
        else if (sounds == false)
        {
            buttonsAudioSource.volume = 0f;
            soundsButtonImage.sprite = toggleOFF;
        }
    }

    private void LoadMusicSettings()
    {
        bool music = YandexGame.savesData.music;

        if (music == true)
        {
            musicButtonImage.sprite = toggleON;
            musicAudioSource.volume = 1f;
        }
        else if (music == false)
        {
            musicAudioSource.Stop();
            musicAudioSource.volume = 0f;
            musicButtonImage.sprite = toggleOFF;
        }
    }

    public void btnSounds()
    {
        bool sounds = YandexGame.savesData.sounds;

        if (sounds == true)
        {
            buttonsAudioSource.volume = 0f;
            soundsButtonImage.sprite = toggleOFF;
            YandexGame.savesData.sounds = false;
        }
        else if (sounds == false)
        {
            buttonsAudioSource.volume = 1f;
            buttonsAudioSource.Play();
            soundsButtonImage.sprite = toggleON;
            YandexGame.savesData.sounds = true;
        }

        YandexGame.SaveProgress();
    }

    public void btnMusic()
    {
        buttonsAudioSource.Play();
        bool music = YandexGame.savesData.music;

        if (music == true)
        {
            musicAudioSource.Pause();
            musicButtonImage.sprite = toggleOFF;
            YandexGame.savesData.music = false;
        }
        else if (music == false)
        {
            musicAudioSource.Play();
            musicButtonImage.sprite = toggleON;
            YandexGame.savesData.music = true;
        }

        YandexGame.SaveProgress();
    }
}
