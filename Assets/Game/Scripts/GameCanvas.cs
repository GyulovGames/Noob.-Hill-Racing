using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using YG;
using Unity.VisualScripting;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance { get; set; }
    public static UnityEvent PauseEvent = new UnityEvent();

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
    [SerializeField] private Sprite toggleON;
    [SerializeField] private Sprite toggleOFF;
    [SerializeField] private Image fuelBarImage;
    [SerializeField] private Slider fuelBarSlider;
    [SerializeField] private AudioSource buttonPlayer;
    [Space(5)]
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

        int coins = int.Parse(coinCounter.text);
        YandexGame.savesData.Coins_sdk += coins;
        YandexGame.SaveProgress();


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
    private IEnumerator DelayLoad(int sceneIndex)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
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
    public void UpdateSoundsSettings()
    {
        bool Sounds = YandexGame.savesData.Sounds_sdk;

        if (Sounds)
        {
            buttonPlayer.volume = 1f;
            soundsToggleImage.sprite = toggleON;
        }
        else
        {
            buttonPlayer.volume = 0f;
            soundsToggleImage.sprite = toggleOFF;
        }
    }
    public void UpdateMusicSettings()
    {
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource musicPlayerAudioSource = musicPlayer.GetComponent<AudioSource>();

        bool Music = YandexGame.savesData.Music_sdk;

        if (Music)
        {
            musicPlayerAudioSource.volume = 1f;
            musicToggleImage.sprite = toggleON;
        }
        else
        {
            musicPlayerAudioSource.Stop();
            musicPlayerAudioSource.volume = 0f;
            musicToggleImage.sprite = toggleOFF;
        }
    }


    public void btn_ChangeSounds()
    {
        bool Sounds = YandexGame.savesData.Sounds_sdk;

        if (Sounds)
        {
            buttonPlayer.volume = 0f;
            soundsToggleImage.sprite = toggleOFF;
            YandexGame.savesData.Sounds_sdk = false;
        }
        else
        {
            buttonPlayer.volume = 1f;
            buttonPlayer.Play();
            soundsToggleImage.sprite = toggleON;
            YandexGame.savesData.Sounds_sdk = true;
        }

        YandexGame.SaveProgress();
    }
    public void btn_ChangeMusic()
    {
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource musicPlayerAudioSource = musicPlayer.GetComponent<AudioSource>();
        bool Music = YandexGame.savesData.Music_sdk;
        buttonPlayer.Play();

        if (Music)
        {
            musicPlayerAudioSource.Pause();
            musicToggleImage.sprite = toggleOFF;
            YandexGame.savesData.Music_sdk = false;
        }
        else
        {
            musicPlayerAudioSource.Play();
            musicToggleImage.sprite = toggleON;
            YandexGame.savesData.Music_sdk = true;
        }

        YandexGame.SaveProgress();
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
        PauseEvent.Invoke();
    }
    public void btn_Resume()
    {
        buttonPlayer.Play();
        fadeController.FadeOut(fadeBackgrounPanel);
        PauseEvent.Invoke();
    }
    public void btn_Restart()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(smothTransitionPanel);
        StartCoroutine(DelayLoad(1));
    }
    public void btn_Home()
    {
        int coins = int.Parse(coinCounter.text);
        YandexGame.savesData.Coins_sdk += coins;
        YandexGame.SaveProgress();

        buttonPlayer.Play();
        moveController.MoveOut(pauseWindow);
        fadeController.FadeIn(smothTransitionPanel);
        StartCoroutine(DelayLoad(7));
    }

    #region PC_Control
    private void Update()
    {
      //  horizontalInput = Input.GetAxis("Horizontal");
    }
    #endregion
    #region MB_Control
    public void onbreakenter() { horizontalInput = -1f; print("b"); }
    public void onbreakexit() { horizontalInput = 0f; print("b"); }

    public void ongasenter() { horizontalInput = 1f; print("b"); }
    public void ongasexit() { horizontalInput = 0f; print("b"); }
    #endregion
}