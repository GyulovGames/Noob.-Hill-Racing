using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance { get; set; }

    [SerializeField] private FadeController fadeController;
    [SerializeField] private MoveController moveController;


    [SerializeField] private Text fuelOutText;
    [SerializeField] private Text driverCrashText;
    [SerializeField] private Text coinCounter;
    [Space(5)]
    [SerializeField] private CanvasGroup smothTransitionPanel;
    [SerializeField] private CanvasGroup fadeBackgrounPanel;
    [SerializeField] private RectTransform pauseWindow;
    [SerializeField] private RectTransform resultWindow;
    [Space(5)]
    [SerializeField] private Image soundsToggleImage;
    [SerializeField] private Image musicToggleImage;
    [SerializeField] private Image fuelBarImage;
    [SerializeField] private Slider fuelBarSlider;
    [SerializeField] private AudioSource buttonPlayer;
    [Space(5)]
    [SerializeField] private Sprite toggleON;
    [SerializeField] private Sprite toggleOFF;
    [SerializeField] private Gradient fuelBarGradient;

    [HideInInspector] public float horizontalInput;



    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this; 
        }
    }

    public void Start()
    {
        RemoveSmothTransition();
        LoadSoundsSettings();
        LoadMusicSettings();
    }

    public void UpdateFuelBarOnStart(float fuelAmmountOnStart)
    {
        fuelBarSlider.maxValue = fuelAmmountOnStart;
        fuelBarSlider.value = fuelAmmountOnStart;
    }
    public IEnumerator OpenResultWindow(string reason)
    {
        yield return new WaitForSeconds(2.75f);
        fadeController.FadeIn(fadeBackgrounPanel);
        moveController.MoveIn(resultWindow);

        if (reason == "Crash")
        {
            driverCrashText.enabled = true;
        }
        else if (reason == "FuelOut")
        {
            fuelOutText.enabled = true;
        }
    }
    public void UpdateFuelbar(float currentFuelValue)
    {
        fuelBarSlider.value = currentFuelValue;
        fuelBarImage.color = fuelBarGradient.Evaluate(fuelBarSlider.normalizedValue);
    }
    private void RemoveSmothTransition()
    {
        fadeController.FadeOut(smothTransitionPanel);
    }
    private void LoadSoundsSettings()
    {
        bool sounds = YandexGame.savesData.Sounds_sdk;

        if (sounds == true)
        {
            buttonPlayer.volume = 1f;
            soundsToggleImage.sprite = toggleON;
        }
        else if (sounds == false)
        {
            buttonPlayer.volume = 0f;
            soundsToggleImage.sprite = toggleOFF;
        }
    }
    private void LoadMusicSettings()
    {
        GameObject musicPlayerObject = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource musicPlayer = musicPlayerObject.GetComponent<AudioSource>();

        bool music = YandexGame.savesData.Music_sdk;

        if (music == true)
        {
            musicPlayer.volume = 1f;
            musicToggleImage.sprite = toggleON;
        }
        else if (music == false)
        {
            musicPlayer.Stop();
            musicPlayer.volume = 0f;
            musicToggleImage.sprite = toggleOFF;
        }
    }
    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void CoinCounter()
    {
        int newnumber = int.Parse(coinCounter.text);
        newnumber += 5;
        coinCounter.text = newnumber.ToString();
    }

    public void btn_Sounds()
    {
        bool sounds = YandexGame.savesData.Sounds_sdk;

        if (sounds == true)
        {
            buttonPlayer.volume = 0f;
            soundsToggleImage.sprite = toggleOFF;
            YandexGame.savesData.Sounds_sdk = false;
        }
        else if (sounds == false)
        {
            buttonPlayer.volume = 1f;
            buttonPlayer.Play();
            soundsToggleImage.sprite = toggleON;
            YandexGame.savesData.Sounds_sdk = true;
        }

        YandexGame.SaveProgress();
    }
    public void btn_Music()
    {
        buttonPlayer.Play();
        GameObject musicplayerObject = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource musicPlayer = musicplayerObject.GetComponent<AudioSource>();

        bool music = YandexGame.savesData.Music_sdk;

        if (music == true)
        {
            musicPlayer.Pause();
            musicToggleImage.sprite = toggleOFF;
            YandexGame.savesData.Music_sdk = false;
        }
        else if (music == false)
        {
            musicPlayer.Play();
            musicToggleImage.sprite = toggleOFF;
            YandexGame.savesData.Music_sdk = true;
        }

        YandexGame.SaveProgress();
    }
    public void btn_Pause()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(fadeBackgrounPanel);
        moveController.MoveIn(pauseWindow);
    }
    public void btn_Resume()
    {
        buttonPlayer.Play();
        fadeController.FadeOut(fadeBackgrounPanel);
        moveController.MoveOut(pauseWindow);
    }
    public void btn_Home()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(smothTransitionPanel);
        moveController.MoveIn(pauseWindow);
        Invoke("LoadMainMenu", 2f);
    }
 
    #region PC_Control
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
    #endregion
    #region MB_Control
    public void onbreakenter() { horizontalInput = -1f; }
    public void onbreakexit() { horizontalInput = 0f; }

    public void ongasenter() { horizontalInput = 1f; }
    public void ongasexit() { horizontalInput = 0f; }
    #endregion
}