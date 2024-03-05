using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private MoveController moveController;
    [SerializeField] private FadeController fadeController;
    [Space(10)]
    [SerializeField] private RectTransform settingsWindow;
    [SerializeField] private RectTransform allGamesWindow;
    [SerializeField] private RectTransform transportWindow;
    [SerializeField] private RectTransform shoppingWindow;
    [Space(5)]
    [SerializeField] private CanvasGroup fadeBackground1;
    [SerializeField] private CanvasGroup fadeBackground2;
    [Space(10)]
    [SerializeField] private AudioSource buttonPlayer;
    [Space(10)]
    [SerializeField] private Image soundsToggleImage;
    [SerializeField] private Image musicToggleImage;
    [SerializeField] private Image purchaseImage;
    [SerializeField] private Sprite onToggleSprite;
    [SerializeField] private Sprite offtoggleSprite;
    [SerializeField] private Text coinIndicator;
    [SerializeField] private GameObject[] carsButtons;
 
    private bool[] Cars = new bool[9];
    private int carsAmount;
    private int coins;

    public void Start()
    {
        DownloadDataFromSDK();
    }

    private void DownloadDataFromSDK()
    {
        coins = YandexGame.savesData.Coins_sdk;
        UpdateCoinsIndicator();

        carsAmount = YandexGame.savesData.Cars_sdk.Length;
        for (int i = 0; i < carsAmount; i++)
        {
            Cars[i] = YandexGame.savesData.Cars_sdk[i];
        }
    }
    private void MarkLockedUnlockedCars()
    {
        for(int i = 0; i < carsAmount;i++)
        {
            if (!Cars[i])
            {
                Transform Price = carsButtons[i].transform.Find("Price");
                Price.gameObject.SetActive(true);
            }
        }
    }
    private void UpdateCoinsIndicator()
    {
        coinIndicator.text = coins.ToString();
        //Maybe some animations, and sounds
    }

    public void ChoseCar(int carIndex)
    {
        if (Cars[carIndex])
        {
            YandexGame.savesData.SelectedCarIndex_sdk = carIndex;
            Transform mark = carsButtons[carIndex].transform.Find("Mark");
            mark.gameObject.SetActive(true);        
        }
        else
        {

        }
    }
    public void btn_OpenTransport()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(fadeBackground1);
        moveController.MoveIn(transportWindow);
        MarkLockedUnlockedCars();
    }
    public void btn_CloseTransport()
    {
        buttonPlayer.Play();
        moveController.MoveOut(transportWindow);
        fadeController.FadeOut(fadeBackground1);
    }
    public void btn_SoundsToggle()
    {
        bool sounds = YandexGame.savesData.Sounds_sdk;

        if (sounds == true)
        {
            buttonPlayer.volume = 0f;
            soundsToggleImage.sprite = offtoggleSprite;
            YandexGame.savesData.Sounds_sdk = false;
        }
        else if (sounds == false)
        {
            buttonPlayer.volume = 1f;
            buttonPlayer.Play();
            soundsToggleImage.sprite = onToggleSprite;
            YandexGame.savesData.Sounds_sdk = true;
        }

        YandexGame.SaveProgress();
    }
    public void btn_MusicToggle()
    {
        buttonPlayer.Play();
        bool music = YandexGame.savesData.Music_sdk;

        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource musicPlayerAudioSource = musicPlayer.GetComponent<AudioSource>();

        if (music == true)
        {
            musicPlayerAudioSource.Pause();
            musicToggleImage.sprite = offtoggleSprite;
            YandexGame.savesData.Music_sdk = false;
        }
        else if (music == false)
        {
            musicPlayerAudioSource.Play();
            musicToggleImage.sprite = onToggleSprite;
            YandexGame.savesData.Music_sdk = true;
        }

        YandexGame.SaveProgress();
    }
    public void btn_OpenSettings()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(fadeBackground1);
        moveController.MoveIn(settingsWindow);
    }
    public void btn_CloseSettings()
    {
        buttonPlayer.Play();
        moveController.MoveOut(settingsWindow);
        fadeController.FadeOut(fadeBackground1);
    }
    public void btn_OpenAllGames()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(fadeBackground1);
        moveController.MoveIn(allGamesWindow);
    }
    public void btn_CloseAllGames()
    {
        buttonPlayer.Play();
        moveController.MoveOut(allGamesWindow);
        fadeController.FadeOut(fadeBackground1);
    }











    private void Update()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
            print("Clear!");
        }
    }
}