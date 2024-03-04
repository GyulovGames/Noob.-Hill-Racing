using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;
    [SerializeField] private MoveController moveController;
    [Space(15)]
    [SerializeField] private CanvasGroup smoothTransitionPanel;
    [SerializeField] private CanvasGroup fadeBackgroundPanel;
    [Space(15)]
    [SerializeField] private RectTransform traillSelectionWindow;
    [SerializeField] private RectTransform vehicleSelectionWindow;
    [SerializeField] private RectTransform upgradeWindow;
    [SerializeField] private RectTransform settingsWindow;
    [SerializeField] private RectTransform allGamesWindow;
    [SerializeField] private AudioSource buttonSoundsPlayerAudioSource;
    [Space(15)]
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image musicButtonImage;
    [SerializeField] private Text coinsIndicator;
    [Space(15)]
    [SerializeField] private Sprite toggleON;
    [SerializeField] private Sprite toggleOFF;

    [SerializeField] private Button engineButton;

    [SerializeField] private GameObject[] prices;
    [SerializeField] private GameObject[] select;
    [SerializeField] private GameObject[] upgradeButtons;
    [SerializeField] private GameObject[] vehiclesArray = new GameObject[0];

    public  int currentCoinsAmmount;
    private string carToUpgrade;

    public void Start()
    {
        YandexGame.savesData.unlockedVehicles[0] = true;


        YandexGame.SaveProgress();

        LoadCoinsNumber();
        LoadSoundsSettings();
        LoadMusicSettings();
        LoadUnlockedVehicles();
        

        CanvasGroup[] disapearGroup = new CanvasGroup[] { smoothTransitionPanel };
        fadeController.Disappear(disapearGroup);

        GameObject parent = GameObject.Find("button;");


    }


    public void MarkAsSelected(int index)
    {
        if (YandexGame.savesData.unlockedVehicles[index] )
        {
            foreach (GameObject obj in select)
            {
                obj.SetActive(false);
            }

            for (int i = 0; i < select.Length; i++)
            {
                if (i == index)
                {
                    select[i].SetActive(true);
                }
            }
        }
    }

    public void btn_ChooseCar(int carIndex)
    {
        if (YandexGame.savesData.unlockedVehicles[carIndex] && !vehiclesArray[carIndex].activeSelf)
        {
            foreach(GameObject car in vehiclesArray)
            {
                car.SetActive(false);
                car.transform.position = new Vector2(0, -1.25f);
            }

            SwichCar(carIndex);
            MarkAsSelected(carIndex);
        }
    }

    private void SwichCar(int index)
    {
        for (int i = 0; i < vehiclesArray.Length; i++)
        {
            if (i == index && YandexGame.savesData.unlockedVehicles[i])
            {
                vehiclesArray[i].SetActive(true);
                carToUpgrade = vehiclesArray[i].name;
            }
        }
    }
    

    public void LoadUnlockedVehicles()
    {
        int vehiclesNumber = YandexGame.savesData.unlockedVehicles.Length;

        for(int i = 0; i < vehiclesNumber;  i++)
        {
            bool unlocked = YandexGame.savesData.unlockedVehicles[i];

            if (!unlocked)
            {
                prices[i].SetActive(true);
            }
        }
    }


    private void UpdateTehnicalSpecificationsBars(int index)
    {
        //switch()

        //Slider engineSlider = GetComponentInChildren<Slider>();
        //engineSlider.value = YandexGame.savesData.upgradesLevel[0];
        //print(YandexGame.savesData.upgradesLevel[0]);
    }

    private void UpdateUpgradeButtonsSlider()
    {

    }

    public void btn_Upgrade(string upgradePart)
    {
        switch (carToUpgrade)
        {
            case "Minecart4x4":
                switch (upgradePart)
                {
                    case "engine":
                        if (YandexGame.savesData.minecart_4x4_UpgradeLevels[0] != 10)
                        {
                            YandexGame.savesData.minecart_4x4_UpgradeLevels[0]++;
                            YandexGame.savesData.minecart4x4Part[0] = 100 + 10 * YandexGame.savesData.minecart_4x4_UpgradeLevels[0];

                            print(YandexGame.savesData.minecart_4x4_UpgradeLevels[0]);
                            print(YandexGame.savesData.minecart4x4Part[0]);

                            GameObject engineButton = upgradeButtons[0];
                            Slider slider = engineButton.GetComponentInChildren<Slider>();
                            slider.value = YandexGame.savesData.minecart_4x4_UpgradeLevels[0];

                            if (YandexGame.savesData.minecart_4x4_UpgradeLevels[0] == 10)
                            {
                                Text priceText = engineButton.GetComponentInChildren<Text>();
                                priceText.text = "MAX";
                            }
                        }
                        break;
                    case "suspension":
                        if (YandexGame.savesData.minecart_4x4_UpgradeLevels[1] != 10)
                        {
                            YandexGame.savesData.minecart_4x4_UpgradeLevels[1]++;
                            YandexGame.savesData.minecart4x4Part[1] = 0.1f * YandexGame.savesData.minecart_4x4_UpgradeLevels[1];
                        }
                        break;
                    case "awd":
                        if (YandexGame.savesData.minecart_4x4_UpgradeLevels[2] != 10)
                        {
                            YandexGame.savesData.minecart_4x4_UpgradeLevels[2]++;
                            YandexGame.savesData.minecart4x4Part[2] = 20 * YandexGame.savesData.minecart_4x4_UpgradeLevels[2];
                        }
                        break;
                    case "wheels":
                        if (YandexGame.savesData.minecart_4x4_UpgradeLevels[3] != 10)
                        {
                            YandexGame.savesData.minecart_4x4_UpgradeLevels[3]++;
                            YandexGame.savesData.minecart4x4Part[3] = 0.22f * YandexGame.savesData.minecart_4x4_UpgradeLevels[3];
                        }
                        break;
                    case "canister":
                        if (YandexGame.savesData.minecart_4x4_UpgradeLevels[4] != 10)
                        {
                            YandexGame.savesData.minecart_4x4_UpgradeLevels[4]++;
                            YandexGame.savesData.minecart4x4Part[4] = 100 + 20 * YandexGame.savesData.minecart_4x4_UpgradeLevels[4];
                        }
                        break;
                }                   
                break;

        }

        YandexGame.SaveProgress();

        //if (YandexGame.savesData.upgradesLevel[0] < 10)
        //{
        //    int nextUpgradeLevel = YandexGame.savesData.upgradesLevel[0];
        //    nextUpgradeLevel++;
        //    float currentEnginePower = YandexGame.savesData.minecartEnginePower * nextUpgradeLevel;

        //    Slider engineSlider = GetComponentInChildren<Slider>();
        //    engineSlider.value = nextUpgradeLevel;

        //    YandexGame.savesData.minecartEnginePower = currentEnginePower;
        //    YandexGame.savesData.upgradesLevel[0] = nextUpgradeLevel;
        //    YandexGame.SaveProgress();

        //    print(nextUpgradeLevel);
        //}
    }

    private void LoadCoinsNumber()
    {
        currentCoinsAmmount = YandexGame.savesData.savedCoins;
        coinsIndicator.text = currentCoinsAmmount.ToString();
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

        CanvasGroup[] appearGroup = new CanvasGroup[] {smoothTransitionPanel };
        fadeController.Appear(appearGroup);
        StartCoroutine(LoadDelay("Level (1) Countryside"));
    }

    private IEnumerator LoadDelay(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);       
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