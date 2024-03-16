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
    [SerializeField] private RectTransform allGamesWindow;
    [SerializeField] private RectTransform settingsWindow;
    [SerializeField] private CanvasGroup smothTransition;
    [SerializeField] private CanvasGroup fadeBack1;
    [SerializeField] private CanvasGroup fadeBack2;
    [Space(5)]
    [SerializeField] private AudioSource buttonPlayer;
    [SerializeField] private Text coinsIndicator;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image musicButtonImage;
    [SerializeField] private Sprite toggleON;
    [SerializeField] private Sprite toggleOFF;
    [Space(5)]
    [SerializeField] private GameObject[] carButtons;
    [SerializeField] private Slider[] upgradeSliders;
    [SerializeField] private Text[] upgradePriceText;
    [SerializeField] private Text[] maximumText;
    [SerializeField] private GameObject[] carsOnScene;

    private bool[] FreeCars = new bool[9];
    private int LastSelectedCar;
    private string purchaseTip;
    private int priceToPay;
    private int RoadToBuy;
    private int CarToBuy;
    private int Coins;
    private bool Sounds;
    private bool Music;


    private void Awake()
    {
        FirstSesion();
        DownloadData();
    }

    private void Start()
    {
        UpdateMusicSettings();
        UpdateSoundsSettings();
        UpdateCoinsIndicator();
        SpawnSelectedCar(LastSelectedCar);
    }

    private void FirstSesion()
    {
        if (YandexGame.savesData.isFirstSession)
        {
            YandexGame.savesData.Car0_Upgrades[1] = 1;
            YandexGame.savesData.Car0_Upgrades[2] = 1;
            YandexGame.savesData.Car0_Upgrades[3] = 1;
            YandexGame.savesData.Car0_Upgrades[4] = 1;
            YandexGame.savesData.Car0_Upgrades[5] = 1;

            YandexGame.savesData.Car1_Upgrades[1] = 1;
            YandexGame.savesData.Car1_Upgrades[2] = 1;
            YandexGame.savesData.Car1_Upgrades[3] = 1;
            YandexGame.savesData.Car1_Upgrades[4] = 1;
            YandexGame.savesData.Car1_Upgrades[5] = 1;

            YandexGame.savesData.Car2_Upgrades[1] = 1;
            YandexGame.savesData.Car2_Upgrades[2] = 1;
            YandexGame.savesData.Car2_Upgrades[3] = 1;
            YandexGame.savesData.Car2_Upgrades[4] = 1;
            YandexGame.savesData.Car2_Upgrades[5] = 1;

            YandexGame.savesData.Car3_Upgrades[1] = 1;
            YandexGame.savesData.Car3_Upgrades[2] = 1;
            YandexGame.savesData.Car3_Upgrades[3] = 1;
            YandexGame.savesData.Car3_Upgrades[4] = 1;
            YandexGame.savesData.Car3_Upgrades[5] = 1;

            YandexGame.savesData.Car4_Upgrades[1] = 1;
            YandexGame.savesData.Car4_Upgrades[2] = 1;
            YandexGame.savesData.Car4_Upgrades[3] = 1;
            YandexGame.savesData.Car4_Upgrades[4] = 1;
            YandexGame.savesData.Car4_Upgrades[5] = 1;

            YandexGame.savesData.Car5_Upgrades[1] = 1;
            YandexGame.savesData.Car5_Upgrades[2] = 1;
            YandexGame.savesData.Car5_Upgrades[3] = 1;
            YandexGame.savesData.Car5_Upgrades[4] = 1;
            YandexGame.savesData.Car5_Upgrades[5] = 1;
            YandexGame.SaveProgress();
        }
    }
    private void DownloadData()
    {
        FreeCars = YandexGame.savesData.FreeCaras_sdk;
        LastSelectedCar = YandexGame.savesData.LastSelectedCar_sdk;
        Coins = YandexGame.savesData.Coins_sdk;
        Sounds = YandexGame.savesData.Sounds_sdk;
        Music = YandexGame.savesData.Music_sdk;
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
        }

        for(int i = 0; i < upgradeSliders.Length; i++)
        {

            if (upgradeSliders[i].value == 10)
            {
                upgradePriceText[i].gameObject.SetActive(false);
                maximumText[i].gameObject.SetActive(true);                                         // ������������ ��� � ������� �������� ��� ������� ������� ��������. � �������� ������� ��� ��������� � ����������� ������ � ������;
                Transform buttonObject = upgradeSliders[i].transform.parent;                                                             // �� ��������� ������.                     // �������� ������� ��� ��������� � ����������� ������ � �������
                Button button = buttonObject.GetComponent<Button>();
                button.interactable = false;
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

        throw new InvalidOperationException("Purchase type is not valid");
    }
    private void UpdateCoinsIndicator()
    {
        coinsIndicator.text = Coins.ToString();
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
    private void ShowADWindow()
    {
        fadeController.FadeIn(fadeBack2);
        moveController.MoveIn(coinsAdWindow);
    }
    private int GetUpgradePrice( int partIndex)
    {
        int price = int.Parse(upgradePriceText[partIndex].text);
        return price;
    }
    public void UpdateSoundsSettings()
    {
        if (Sounds)
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

        if (Music)
        {
            musicPlayerAudioSource.volume = 1f;
            musicButtonImage.sprite = toggleON;
        }
        else
        {
            musicPlayerAudioSource.Stop();
            musicPlayerAudioSource.volume = 0f;
            musicButtonImage.sprite = toggleOFF;
        }
    }



    public void btn_UpgradeButtons(int partIndex)
    {
        buttonPlayer.Play();
        int upgradePrice = GetUpgradePrice(partIndex);

        if(Coins >= upgradePrice)
        {
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
                            print("Yea");
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
            }
        }
        else
        {
            fadeController.FadeIn(fadeBack2);
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

        }
        else if( Coins >= GetPurcahsePrice(index))
        {
            fadeController.FadeIn(fadeBack2);
            moveController.MoveIn(shoppingWindow);
            CarToBuy = index;
        }
        else
        {
            ShowADWindow();
        }
    }
    public void btn_ConfirmPurchase_Yes()
    {
        buttonPlayer.Play();

        FreeCars[CarToBuy] = true;
        moveController.MoveOut(shoppingWindow);
        fadeController.FadeOut(fadeBack2);
        Coins = Coins - priceToPay;
        UpdateCoinsIndicator();
        UpdateFreeCars();
        YandexGame.savesData.FreeCaras_sdk[CarToBuy] = true;
        YandexGame.SaveProgress();
    }
    public void btn_ConfirmPurchase_No()
    {
        buttonPlayer.Play();

        moveController.MoveOut(shoppingWindow);
        fadeController.FadeOut(fadeBack2);
    }
    public void btn_OpenCasrsWindow()
    {
        buttonPlayer.Play();

        fadeController.FadeIn(fadeBack1);
        moveController.MoveIn(carsWindow);
        UpdateFreeCars();
    }
    public void btn_CloseCarsWindow()
    {
        buttonPlayer.Play();

        moveController.MoveOut(carsWindow);
        fadeController.FadeOut(fadeBack1);
    }
    public void btn_CloseADWindow()
    {
        buttonPlayer.Play();

        fadeController.FadeOut(fadeBack2);
        moveController.MoveOut(coinsAdWindow);
    }
    public void btn_OpenAllGames()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(fadeBack1);
        moveController.MoveIn(allGamesWindow);
    }
    public void btn_CloseAllGAmes()
    {
        buttonPlayer.Play();
        moveController.MoveOut(allGamesWindow);
        fadeController.FadeOut(fadeBack1);
    }
    public void btn_OpenSettings()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(fadeBack1);
        moveController.MoveIn(settingsWindow);
    }
    public void btn_CloseSettings()
    {
        buttonPlayer.Play();
        moveController.MoveOut(settingsWindow);
        fadeController.FadeOut(fadeBack1);
    }
    public void btn_ChangeSounds()
    {
        if (Sounds)
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

        buttonPlayer.Play();

        if (Music)
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
    public void btn_Play()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(smothTransition);
        Invoke("DelayLoad", 1f);
    }

    private void DelayLoad()
    {
        SceneManager.LoadScene(1);
    }






    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            YandexGame.savesData.Car0_Upgrades[0] = 1;
            YandexGame.savesData.Car0_Upgrades[1] = 1;
            YandexGame.savesData.Car0_Upgrades[2] = 1;
            YandexGame.savesData.Car0_Upgrades[3] = 1;
            YandexGame.savesData.Car0_Upgrades[4] = 1;
            YandexGame.SaveProgress();
            print("Save Progress!");
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
            print("Reset Progress!");
        }
    }
}