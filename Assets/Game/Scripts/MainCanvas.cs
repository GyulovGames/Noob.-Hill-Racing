using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource buttonsAudioSorce;
    [Space(15)]
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image musicButtonImage;
    [Space(15)]
    [SerializeField] private Sprite toggleOnSprite;
    [SerializeField] private Sprite toggleOffSprite;
 

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
            buttonsAudioSorce.volume = 1f;
            soundsButtonImage.sprite = toggleOnSprite;
        }
        else if (sounds == false)
        {
            buttonsAudioSorce.volume = 0f;
            soundsButtonImage.sprite = toggleOffSprite;
        }
    }

    private void LoadMusicSettings()
    {
        bool music = YandexGame.savesData.music;

        if (music == true)
        {
            musicButtonImage.sprite = toggleOnSprite;
            musicAudioSource.volume = 1f;
        }
        else if (music == false)
        {
            musicAudioSource.Stop();
            musicAudioSource.volume = 0f;
            musicButtonImage.sprite = toggleOffSprite;
        }
    }
}