using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private RectTransform coinsADWindow;
    [Space(5)]
    [SerializeField] private CanvasGroup fadeBackground1;
    [SerializeField] private CanvasGroup fadeBackground2;
    [Space(10)]
    [SerializeField] private AudioSource buttonPlayer;
    [Space(10)]
    [SerializeField] private Image soundsToggleImage;
    [SerializeField] private Image musicToggleImage;
    [SerializeField] private Sprite onToggleSprite;
    [SerializeField] private Sprite offtoggleSprite;
    [SerializeField] private Text coinIndicator;
    [SerializeField] private GameObject[] upgradeButtons;
    [SerializeField] private GameObject[] carsButtons;
    [SerializeField] private GameObject[] carsArray;

    private int[,] CarUpgrades = new int[9, 5];
    private bool[] Cars = new bool[9];
    private int currentPurcahsePrice;
    private int currentPurchaseIndex;
    private int selectedCarIndex;
    private int carsAmount;
    private int coins;

    public void Start()
    {
        DownloadDataFromSDK();
    }


    private void DownloadDataFromSDK()
    {
        selectedCarIndex = YandexGame.savesData.SelectedCarIndex_sdk;
        carsAmount = YandexGame.savesData.Cars_sdk.Length;
        coins = YandexGame.savesData.Coins_sdk;

        for (int i = 0; i < CarUpgrades.GetLength(0); i++)
        {
            for (int y = 0; y < CarUpgrades.GetLength(1); y++)
            {
                CarUpgrades[i, y] = YandexGame.savesData.CarUpgrades_sdk[i, y];
            }
        }

        SpawnSelectedCar(selectedCarIndex);
        UpdateUpgradeSlidders(selectedCarIndex);
        UpdateCoinsIndicator();

        print(CarUpgrades[0, 4]);
    }

    private void UpdateUpgradeSlidders(int carIndex)
    {
        for (int y = 0; y < upgradeButtons.Length; y++)
        {
            Transform btnTransform = upgradeButtons[y].transform;
            Slider btnSlider = btnTransform.GetComponentInChildren<Slider>();
            btnSlider.value = CarUpgrades[carIndex, y];
        }
    }
    private void SpawnSelectedCar(int carIndex)
    {
        GameObject car = carsArray[carIndex];

        if (!car.activeInHierarchy)
        {
            foreach (GameObject carInArray in carsArray)
            {
                carInArray.SetActive(false);
            }

            car.transform.position = new Vector2(0, 1.5f);
            car.SetActive(true);
            UpdateUpgradeSlidders(carIndex);
        }
    }
    private void MarkLockedUnlockedCars()
    {
        for (int i = 0; i < carsAmount; i++)
        {
            if (!Cars[i])
            {
                Transform Price = carsButtons[i].transform.Find("Price");
                Price.gameObject.SetActive(true);
            }
            else
            {
                Transform Price = carsButtons[i].transform.Find("Price");
                Price.gameObject.SetActive(false);
            }
        }
    }
    private void UpdateCoinsIndicator()
    {
        coinIndicator.text = coins.ToString();
        //Maybe some animations, and sounds
    }
    private int GetCarPrice(int carIndex)
    {
        Transform Price = carsButtons[carIndex].transform.Find("Price");
        Text priceText = Price.GetComponentInChildren<Text>();
        int price = int.Parse(priceText.text);
        currentPurcahsePrice = price;
        currentPurchaseIndex = carIndex;
        return price;
    }

    public void btn_UpgradeButtons(int buttonIndex)
    {
        print(selectedCarIndex);
        CarUpgrades[selectedCarIndex, buttonIndex] += 1;
        YandexGame.savesData.CarUpgrades_sdk[selectedCarIndex, buttonIndex] += 1;
    }
    public void btn_CloseAdWindow()
    {
        moveController.MoveOut(coinsADWindow);
        fadeController.FadeOut(fadeBackground2);
    }
    public void btn_ChoseCar(int carIndex)
    {
        if (Cars[carIndex])
        {
            Transform oldMark = carsButtons[selectedCarIndex].transform.Find("Mark");
            oldMark.gameObject.SetActive(false);

            Transform newMark = carsButtons[carIndex].transform.Find("Mark");
            newMark.gameObject.SetActive(true);

            selectedCarIndex = carIndex;
            YandexGame.savesData.SelectedCarIndex_sdk = carIndex;
            YandexGame.SaveProgress();

            SpawnSelectedCar(carIndex);

        }
        else if (!Cars[carIndex] && coins >= GetCarPrice(carIndex))
        {
            fadeController.FadeIn(fadeBackground2);
            moveController.MoveIn(shoppingWindow);
        }
        else if (!Cars[carIndex] && coins < GetCarPrice(carIndex))
        {
            fadeController.FadeIn(fadeBackground2);
            moveController.MoveIn(coinsADWindow);
        }
    }
    public void btn_PurchaseConfirmationYES()
    {
        coins = coins - currentPurcahsePrice;
        YandexGame.savesData.Coins_sdk = coins;
        UpdateCoinsIndicator();

        Cars[currentPurchaseIndex] = true;
        YandexGame.savesData.Cars_sdk[currentPurchaseIndex] = true;
        MarkLockedUnlockedCars();

        moveController.MoveOut(shoppingWindow);
        fadeController.FadeOut(fadeBackground2);

        YandexGame.SaveProgress();
    }
    public void btn_PurchaseConfirmationNO()
    {
        moveController.MoveOut(shoppingWindow);
        fadeController.FadeOut(fadeBackground2);
    }
    public void btn_OpenTransport()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(fadeBackground1);
        moveController.MoveIn(transportWindow);
        MarkLockedUnlockedCars();
        //  btn_ChoseCar(selectedCarIndex);
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