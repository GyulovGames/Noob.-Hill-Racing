using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using YG;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance { get; set; }


    [SerializeField] private FadeController fadeController;
    [SerializeField] private MoveController moveController;
    [Space(5)]
    [SerializeField] private Text fuelOutText;
    [SerializeField] private Text driverCrashText;
    [SerializeField] private Text coinCounter;
    [SerializeField] private Text resultCoinsText;
    [Space(5)]
    [SerializeField] private CanvasGroup smothTransition;
    [SerializeField] private CanvasGroup fadeBackgroun;
    [SerializeField] private RectTransform pauseWindow;
    [SerializeField] private RectTransform resultWindow;
    [SerializeField] private GameObject MB_Control;
    [Space(5)]
    [SerializeField] private Image soundsToggleImage;
    [SerializeField] private Image musicToggleImage;
    [SerializeField] private Image fuelBarImage;
    [SerializeField] private Sprite toggleON;
    [SerializeField] private Sprite toggleOFF;
    [SerializeField] private Slider fuelBarSlider;
    [SerializeField] private AudioSource buttonPlayer;
    [SerializeField] private AudioSource purchasePlayer;
    [SerializeField] private Gradient fuelBarGradient;

    [HideInInspector] public float horizontalInput;
    private bool userDevice = true;

    public static UnityEvent PauseEvent = new UnityEvent();


    private void OnEnable() => YandexGame.CloseVideoEvent += Reward;
    private void OnDisable() => YandexGame.CloseVideoEvent -= Reward;


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
        UpdateSoundsSettings();
        UpdateMusicSettings();
        DefineUserDevice();
    }

    private void Reward()
    {
        purchasePlayer.Play();
        int x3_Coins = int.Parse(resultCoinsText.text);
        x3_Coins = x3_Coins * 3;
        resultCoinsText.text = x3_Coins.ToString();
        YandexGame.savesData.Coins_sdk += x3_Coins;
        YandexGame.SaveProgress();
    }
    public void UpdateFuelBarOnStart(float fuelAmmountOnStart)
    {
        fuelBarSlider.maxValue = fuelAmmountOnStart;
        fuelBarSlider.value = fuelAmmountOnStart;
    }
    public IEnumerator OpenResultWindow(string reason)
    {
        yield return new WaitForSeconds(2.75f);
        fadeController.FadeIn(fadeBackgroun);
        moveController.MoveIn(resultWindow);

        int coins = int.Parse(coinCounter.text);
        YandexGame.savesData.Coins_sdk += coins;
        YandexGame.SaveProgress();

        resultCoinsText.text = coins.ToString();


        if (reason == "Crash")
        {
            driverCrashText.gameObject.SetActive(true);
        }
        else if (reason == "FuelOut")
        {
            fuelOutText.gameObject.SetActive(true);
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
        fadeController.FadeOut(smothTransition);
    }
    private void DefineUserDevice()
    {
        string isDesctop = YandexGame.EnvironmentData.deviceType.ToString();

        switch (isDesctop)
        {
            case "desktop":
                this.userDevice = true;
                MB_Control.SetActive(false);
                break;
            case "mobile":
                this.userDevice = false;
                MB_Control.SetActive(true);
                break;
            case "tablet":
                this.userDevice = false;
                MB_Control.SetActive(true);
                break;
            default:
                this.userDevice = false;
                MB_Control.SetActive(true);
                break;
        }
    }
    public void CoinCounter()
    {
        int newnumber = int.Parse(coinCounter.text);
        newnumber += 5;
        coinCounter.text = newnumber.ToString();
    }

    public void UpdateSoundsSettings()
    {
        AudioSource[] allAudios = FindObjectsOfType<AudioSource>();

        bool sounds = YandexGame.savesData.Sounds_sdk;

        if (sounds)
        {
            soundsToggleImage.sprite = toggleON;

            foreach (AudioSource audioSource in allAudios)
            {
                if (audioSource.gameObject.tag != "MusicPlayer")
                {
                    audioSource.volume = 1;
                }
            }
        }
        else
        {
            soundsToggleImage.sprite = toggleOFF;

            foreach (AudioSource audioSource in allAudios)
            {
                if (audioSource.gameObject.tag != "MusicPlayer")
                {
                    audioSource.volume = 0;
                }
            }
        }
    }
    public void UpdateMusicSettings()
    {
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource musicPlayerAudioSource = musicPlayer.GetComponent<AudioSource>();
        bool music = YandexGame.savesData.Music_sdk;

        if (music)
        {
            if (!musicPlayerAudioSource.isPlaying)
            {
                musicPlayerAudioSource.Play();
            }

            musicToggleImage.sprite = toggleON;
        }
        else
        {
            musicPlayerAudioSource.Pause();
            musicToggleImage.sprite = toggleOFF;
        }
    }

    public void btn_RewardMultyplay()
    {
        YandexGame.RewVideoShow(0);
    }
    public void btn_ChangeSounds()
    {
        AudioSource[] allAudios = FindObjectsOfType<AudioSource>();

        bool sounds = YandexGame.savesData.Sounds_sdk;

        if (sounds)
        {
            soundsToggleImage.sprite = toggleOFF;
            YandexGame.savesData.Sounds_sdk = false;

            foreach (AudioSource audioSource in allAudios)
            {
                if (audioSource.gameObject.tag != "MusicPlayer")
                {
                    audioSource.volume = 0;
                }
            }
        }
        else
        {
            buttonPlayer.Play();
            soundsToggleImage.sprite = toggleON;
            YandexGame.savesData.Sounds_sdk = true;

            foreach (AudioSource audioSource in allAudios)
            {
                if (audioSource.gameObject.tag != "MusicPlayer")
                {
                    audioSource.volume = 1;
                }
            }
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
    public void btn_Pause()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(fadeBackgroun);
        moveController.MoveIn(pauseWindow);
        PauseEvent.Invoke();
    }
    public void btn_Resume()
    {
        buttonPlayer.Play();
        fadeController.FadeOut(fadeBackgroun);
        moveController.MoveOut(pauseWindow);
        PauseEvent.Invoke();
    }
    public void btn_Restart()
    {
        buttonPlayer.Play();
        fadeController.FadeIn(smothTransition);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(DelayLoad(sceneIndex));
    }
    public void btn_Home()
    {
        int coins = int.Parse(coinCounter.text);
        YandexGame.savesData.Coins_sdk += coins;
        YandexGame.SaveProgress();

        buttonPlayer.Play();
        moveController.MoveOut(pauseWindow);
        fadeController.FadeIn(smothTransition);
        StartCoroutine(DelayLoad(0));
    }

    #region PC_Control
    private void Update()
    {
        if (userDevice)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
    }
    #endregion
    #region MB_Control
    public void onbreakenter() { horizontalInput = -1f;}
    public void onbreakexit() { horizontalInput = 0f;}

    public void ongasenter() { horizontalInput = 1f;}
    public void ongasexit() { horizontalInput = 0f;}
    #endregion
}