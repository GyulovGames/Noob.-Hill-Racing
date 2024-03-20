using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private MoveController moveController;
    [SerializeField] private FadeController fadeController;
    [Space(5)]
    [SerializeField] private RectTransform carsWindow;
    [SerializeField] private RectTransform shoppingWindow;
    [SerializeField] private RectTransform coinsAdWindow;
    [SerializeField] private RectTransform trailsWindow;
    [SerializeField] private RectTransform allGamesWindow;
    [SerializeField] private RectTransform settingsWindow;
    [SerializeField] private CanvasGroup smothTransition;
    [SerializeField] private CanvasGroup fadeBackground1;
    [SerializeField] private CanvasGroup fadeBackground2;
    [Space(5)]
    [SerializeField] private AudioSource buttonPlayer;
    [SerializeField] private AudioSource purchasePlayer;
    [SerializeField] private Text coinsIndicatorText;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image musicButtonImage;
    [SerializeField] private Image carButtonCover;
    [SerializeField] private Image trailButtonCover;
    [SerializeField] private Sprite toggleON;
    [SerializeField] private Sprite toggleOFF;
    [Space(5)]
    [SerializeField] private GameObject[] carButtons;
    [SerializeField] private GameObject[] trailButtons;
    [SerializeField] private Slider[] upgradeSliders;
    [SerializeField] private Text[] upgradePriceText;
    [SerializeField] private Text[] maximumText;
    [SerializeField] private GameObject[] carsOnScene;
    [SerializeField] private Sprite[] carImages;
    [SerializeField] private Sprite[] trailImages;

    private bool[] FreeCars = new bool[6];
    private bool[] FreeTrails = new bool[8];
    private int LastSelectedCar;
    private int LaseSelectedTrail;
    private string purchaseTip;
    private int priceToPay;
    private int TrailToBuy;
    private int CarToBuy;
    private int Coins;


    private void OnEnable() => YandexGame.CloseVideoEvent += Reward;
    private void OnDisable() => YandexGame.CloseVideoEvent -= Reward;

    private void Awake()
    {
        DownloadData();
    }

    private void Start()
    {
        UpdateMusicSettings();
        UpdateSoundsSettings();
        UpdateCoinsIndicator();
        UpdateButtonsCovers();
        SpawnSelectedCar(LastSelectedCar);
        RemoveSmothTransition();
    }

    private void RemoveSmothTransition()
    {
        fadeController.FadeOut(smothTransition);
    }
    private void Reward()
    {
        purchasePlayer.Play();
        Coins += 3000;
        YandexGame.savesData.Coins_sdk += Coins;
        YandexGame.SaveProgress();
        UpdateCoinsIndicator();

        fadeController.FadeOut(fadeBackground2);
        moveController.MoveOut(coinsAdWindow);
    }
    public void UpdateButtonsCovers()
    {
        carButtonCover.sprite = carImages[LastSelectedCar];
        trailButtonCover.sprite = trailImages[LaseSelectedTrail];
    }
    private void DownloadData()
    {
        Coins = YandexGame.savesData.Coins_sdk;
        FreeCars = YandexGame.savesData.FreeCaras_sdk;
        FreeTrails = YandexGame.savesData.FreeTrails_sdk;
        LastSelectedCar = YandexGame.savesData.LastSelectedCar_sdk;
        LaseSelectedTrail = YandexGame.savesData.LastSelectedTrail_sdk;
    }
    private void UpdateUpgradeSliders(int carIndex)
    {       
        switch (carIndex)
        {
            case 0:
                upgradeSliders[0].value = YandexGame.savesData.Car0_Upgrades[0];
                upgradeSliders[1].value = YandexGame.savesData.Car0_Upgrades[1];
                upgradeSliders[2].value = YandexGame.savesData.Car0_Upgrades[2];
                upgradeSliders[3].value = YandexGame.savesData.Car0_Upgrades[3];
                upgradeSliders[4].value = YandexGame.savesData.Car0_Upgrades[4];
                break;
            case 1:
                upgradeSliders[0].value = YandexGame.savesData.Car1_Upgrades[0];
                upgradeSliders[1].value = YandexGame.savesData.Car1_Upgrades[1];
                upgradeSliders[2].value = YandexGame.savesData.Car1_Upgrades[2];
                upgradeSliders[3].value = YandexGame.savesData.Car1_Upgrades[3];
                upgradeSliders[4].value = YandexGame.savesData.Car1_Upgrades[4];
                break;
            case 2:
                upgradeSliders[0].value = YandexGame.savesData.Car2_Upgrades[0];
                upgradeSliders[1].value = YandexGame.savesData.Car2_Upgrades[1];
                upgradeSliders[2].value = YandexGame.savesData.Car2_Upgrades[2];
                upgradeSliders[3].value = YandexGame.savesData.Car2_Upgrades[3];
                upgradeSliders[4].value = YandexGame.savesData.Car2_Upgrades[4];
                break;
            case 3:
                upgradeSliders[0].value = YandexGame.savesData.Car3_Upgrades[0];
                upgradeSliders[1].value = YandexGame.savesData.Car3_Upgrades[1];
                upgradeSliders[2].value = YandexGame.savesData.Car3_Upgrades[2];
                upgradeSliders[3].value = YandexGame.savesData.Car3_Upgrades[3];
                upgradeSliders[4].value = YandexGame.savesData.Car3_Upgrades[4];
                break;
            case 4:
                upgradeSliders[0].value = YandexGame.savesData.Car4_Upgrades[0];
                upgradeSliders[1].value = YandexGame.savesData.Car4_Upgrades[1];
                upgradeSliders[2].value = YandexGame.savesData.Car4_Upgrades[2];
                upgradeSliders[3].value = YandexGame.savesData.Car4_Upgrades[3];
                upgradeSliders[4].value = YandexGame.savesData.Car4_Upgrades[4];
                break;
            case 5:
                upgradeSliders[0].value = YandexGame.savesData.Car5_Upgrades[0];
                upgradeSliders[1].value = YandexGame.savesData.Car5_Upgrades[1];
                upgradeSliders[2].value = YandexGame.savesData.Car5_Upgrades[2];
                upgradeSliders[3].value = YandexGame.savesData.Car5_Upgrades[3];
                upgradeSliders[4].value = YandexGame.savesData.Car5_Upgrades[4];
                break;
        }

        for(int i = 0; i < upgradeSliders.Length; i++)
        {

            if (upgradeSliders[i].value == 10)
            {
                upgradePriceText[i].gameObject.SetActive(false);
                maximumText[i].gameObject.SetActive(true);                                         // использовать изо в функции прокачки для каждого индекса отдельно. И отдельно функции для активации и деактивации кнопок и текста;
                Transform buttonObject = upgradeSliders[i].transform.parent;                                                             // не нагружать циклом.                     // отдельно функции для активации и деактивации кнопок и текстов
                Button button = buttonObject.GetComponent<Button>();
                button.interactable = false;
            }
            else
            {
                upgradePriceText[i].gameObject.SetActive(true);
                maximumText[i].gameObject.SetActive(false);
                Transform buttonObject = upgradeSliders[i].transform.parent;                                                             // не нагружать циклом.                     // отдельно функции для активации и деактивации кнопок и текстов
                Button button = buttonObject.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }
    private void SpawnSelectedCar(int carIndex)
    {
        GameObject lastCar = carsOnScene[LastSelectedCar];
        lastCar.SetActive(false);

        GameObject newCar = carsOnScene[carIndex];
        newCar.transform.rotation = Quaternion.Euler(0, 0, 0);
        newCar.transform.position = new Vector2(0, 2f);
        newCar.SetActive(true);
        LastSelectedCar = carIndex;
        UpdateUpgradeSliders(LastSelectedCar);
        YandexGame.savesData.LastSelectedCar_sdk = carIndex;
        YandexGame.SaveProgress();
    }
    private int GetPurcahsePrice(int index)
    {
        if(purchaseTip == "Car")
        {
            Transform priceMark = carButtons[index].transform.Find("Price");
            Text text = priceMark.GetComponentInChildren<Text>();
            priceToPay = int.Parse(text.text);           
            return priceToPay;
        }
        else if(purchaseTip == "Trail")
        {
            Transform priceMark = trailButtons[index].transform.Find("Price");
            Text text = priceMark.GetComponentInChildren<Text>();
            priceToPay = int.Parse(text.text);
            return priceToPay;
        }

        throw new InvalidOperationException("Purchase type is not valid");
    }
    private void UpdateCoinsIndicator()
    {
        coinsIndicatorText.text = Coins.ToString();
        YandexGame.savesData.Coins_sdk = Coins;
        YandexGame.SaveProgress();
    }
    private void UpdateFreeCars()
    {
        for (int i = 0; i < FreeCars.Length; i++)
        {
            if (FreeCars[i])
            {
                Transform price = carButtons[i].transform.Find("Price");
                price.gameObject.SetActive(false);
            }

            if (i == LastSelectedCar)
            {
                Transform mark = carButtons[i].transform.Find("Mark");
                mark.gameObject.SetActive(true);
            }
        }
    }
    private void UpdateFreeTrails()
    {
        for(int i = 1; i < FreeTrails.Length; i++)
        {
            if (FreeTrails[i])
            {
                Transform price = trailButtons[i].transform.Find("Price");
                price.gameObject.SetActive(false);
            }

            if(i == LaseSelectedTrail)
            {
                Transform mark = trailButtons[i].transform.Find("Mark");
                mark.gameObject.SetActive(true);
            }
        }
    }
    private void ShowADWindow()
    {
        fadeController.FadeIn(fadeBackground2);
        moveController.MoveIn(coinsAdWindow);
    }
    private int GetUpgradePrice( int partIndex)
    {
        int price = int.Parse(upgradePriceText[partIndex].text);
        return price;
    }

    public void UpdateSoundsSettings()
    {
        bool sounds = YandexGame.savesData.Sounds_sdk;

        if (sounds)
        {
            buttonPlayer.volume = 1f;
            soundsButtonImage.sprite = toggleON;
        }
        else
        {
            buttonPlayer.volume = 0f;
            soundsButtonImage.sprite = toggleOFF;
        }
    }
    public void UpdateMusicSettings()
    {
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource musicPlayerAudioSource = musicPlayer.GetComponent<AudioSource>();
        bool music = YandexGame.savesData.Music_sdk;

        if (music)
        {
            if(!musicPlayerAudioSource.isPlaying)
            {
                musicPlayerAudioSource.Play();
            }

            musicButtonImage.sprite = toggleON;
        }
        else
        {
            musicPlayerAudioSource.Pause();
            musicButtonImage.sprite = toggleOFF;
        }
    }


    public void btn_UpgradeButtons(int partIndex)
    {
        buttonPlayer.Play();
        int upgradePrice = GetUpgradePrice(partIndex);

        if(Coins >= upgradePrice)
        {
            purchasePlayer.Play();

            Coins = Coins - upgradePrice;
            UpdateCoinsIndicator();

            switch (LastSelectedCar)
            {
                case 0:
                    switch (partIndex)
                    {
                        case 0:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car0_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 1:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car0_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 2:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car0_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 3:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car0_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 4:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car0_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                    }
                    break;
                case 1:
                    switch (partIndex)
                    {
                        case 0:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car1_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 1:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car1_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 2:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car1_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 3:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car1_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 4:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car1_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                    }
                    break;
                case 2:
                    switch (partIndex)
                    {
                        case 0:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car2_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 1:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car2_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 2:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car2_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 3:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car2_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 4:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car2_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                    }
                    break;
                case 3:
                    switch (partIndex)
                    {
                        case 0:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car3_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 1:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car3_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 2:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car3_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 3:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car3_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 4:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car3_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                    }
                    break;
                case 4:
                    switch (partIndex)
                    {
                        case 0:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car4_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 1:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car4_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 2:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car4_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 3:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car4_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 4:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car4_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                    }
                    break;
                case 5:
                    switch (partIndex)
                    {
                        case 0:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car5_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 1:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car5_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 2:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car5_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 3:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car5_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                        case 4:
                            upgradeSliders[partIndex].value += 1;
                            YandexGame.savesData.Car5_Upgrades[partIndex] += 1;
                            YandexGame.SaveProgress();
                            UpdateUpgradeSliders(LastSelectedCar);
                            break;
                    }
                    break;
            }
        }
        else
        {
            fadeController.FadeIn(fadeBackground2);
            moveController.MoveIn(coinsAdWindow);
        }
    }
    public void btn_ChoseACar(int index)
    {
        buttonPlayer.Play();
        purchaseTip = "Car";

        if (FreeCars[index])
        {
            Transform oldMark = carButtons[LastSelectedCar].transform.Find("Mark");
            Transform newMark = carButtons[index].transform.Find("Mark");
            oldMark.gameObject.SetActive(false);
            newMark.gameObject.SetActive(true);
            SpawnSelectedCar(index);
            UpdateButtonsCovers();
        }
        else if( Coins >= GetPurcahsePrice(index))
        {
            fadeController.FadeIn(fadeBackground2);
            moveController.MoveIn(shoppingWindow);
            CarToBuy = index;
        }
        else
        {
            ShowADWindow();
        }
    }
    public void btn_ChoseATrail(int index)
    {
        buttonPlayer.Play();
        purchaseTip = "Trail";

        if (FreeTrails[index])
        {
            Transform oldMark = trailButtons[LaseSelectedTrail].transform.Find("Mark");
            Transform newMark = trailButtons[index].transform.Find("Mark");
            oldMark.gameObject.SetActive(false);
            newMark.gameObject.SetActive(true);
            LaseSelectedTrail = index;

            YandexGame.savesData.LastSelectedTrail_sdk = index;
            YandexGame.SaveProgress();
            UpdateButtonsCovers();
        }
        else if(Coins >= GetPurcahsePrice(index))
        {
            fadeController.FadeIn(fadeBackground2);
            moveController.MoveIn(shoppingWindow);
            TrailToBuy = index;
        }
        else
        {
            ShowADWindow();
        }
    }
    public void btn_ConfirmPurchase_Yes()
    {
        buttonPlayer.Play();
        purchasePlayer.Play();


        if (purchaseTip == "Car")
        {
            FreeCars[CarToBuy] = true;
            moveController.MoveOut(shoppingWindow);
            fadeController.FadeOut(fadeBackground2);
            Coins = Coins - priceToPay;
            UpdateCoinsIndicator();
            UpdateFreeCars();
            YandexGame.savesData.FreeCaras_sdk[CarToBuy] = true;
            YandexGame.SaveProgress();
        }
        else if(purchaseTip == "Trail")
        {
            FreeTrails[TrailToBuy] = true;
            moveController.MoveOut(shoppingWindow);
            fadeController.FadeOut(fadeBackground2);
            Coins = Coins - priceToPay;
            UpdateCoinsIndicator();
            UpdateFreeTrails();
            YandexGame.savesData.FreeTrails_sdk[TrailToBuy] = true;
            YandexGame.SaveProgress();
        }
    }
    public void btn_ConfirmPurchase_No()
    {
        buttonPlayer.Play();

        moveController.MoveOut(shoppingWindow);
        fadeController.FadeOut(fadeBackground2);
    }
    public void btn_OpenTrailsWindow()
    {
        buttonPlayer.Play();

        fadeController.FadeIn(fadeBackground1);
        moveController.MoveIn(trailsWindow);
        UpdateFreeTrails();
    }
    public void btn_CloseTrailsWindow()
    {
        buttonPlayer.Play();

        moveController.MoveOut(trailsWindow);
        fadeController.FadeOut(fadeBackground1);
    }
    public void btn_OpenCasrsWindow()
    {
        buttonPlayer.Play();

        fadeController.FadeIn(fadeBackground1);
        moveController.MoveIn(carsWindow);
        UpdateFreeCars();
    }
    public void btn_CloseCarsWindow()
    {
        buttonPlayer.Play();

        moveController.MoveOut(carsWindow);
        fadeController.FadeOut(fadeBackground1);
    }
    public void btn_CloseADWindow()
    {
        buttonPlayer.Play();

        fadeController.FadeOut(fadeBackground2);
        moveController.MoveOut(coinsAdWindow);
    }
    public void btn_OpenAllGames()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(fadeBackground1);
        moveController.MoveIn(allGamesWindow);
    }
    public void btn_CloseAllGAmes()
    {
        buttonPlayer.Play();
        moveController.MoveOut(allGamesWindow);
        fadeController.FadeOut(fadeBackground1);
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
    public void btn_ChangeSounds()
    {
        bool sounds = YandexGame.savesData.Sounds_sdk;

        if (sounds)
        {
            buttonPlayer.volume = 0f;
            soundsButtonImage.sprite = toggleOFF;
            YandexGame.savesData.Sounds_sdk = false;
        }
        else
        {
            buttonPlayer.volume = 1f;
            buttonPlayer.Play();
            soundsButtonImage.sprite = toggleON;
            YandexGame.savesData.Sounds_sdk = true;
        }

        YandexGame.SaveProgress();
    }
    public void btn_ChangeMusic()
    {     
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource musicPlayerAudioSource = musicPlayer.GetComponent<AudioSource>();
        bool music = YandexGame.savesData.Music_sdk;

        buttonPlayer.Play();

        if (music)
        {
            musicPlayerAudioSource.Pause();
            musicButtonImage.sprite = toggleOFF;
            YandexGame.savesData.Music_sdk = false;
        }
        else 
        {
            musicPlayerAudioSource.Play();
            musicButtonImage.sprite = toggleON;
            YandexGame.savesData.Music_sdk = true;
        }

        YandexGame.SaveProgress();
    }
    public void btn_RewardedShow()
    {
        buttonPlayer.Play();
        YandexGame.RewVideoShow(0);
    }
    public void btn_Play()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(smothTransition);
        Invoke("DelayLoad", 1f);
    }
    private void DelayLoad()
    {
        SceneManager.LoadScene(LaseSelectedTrail);
    }
}