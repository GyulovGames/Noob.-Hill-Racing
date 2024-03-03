using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;
using System.Runtime.CompilerServices;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas InstanceGC { get; set; }

    [SerializeField] private FadeController fadeController;
    [SerializeField] private MoveController moveController;
    [SerializeField] private Text crashWariantCrash;
    [SerializeField] private Text crashWariant2Fuel;
    [SerializeField] private Text coinscounter;
    [Space(15)]
    [SerializeField] private CanvasGroup smoothTransitionPanel;
    [SerializeField] private CanvasGroup fadeBackgroundPanel;
    [Space(15)]
    [SerializeField] private AudioSource buttonSoundsPlayerAudioSource;
    [SerializeField] private RectTransform pauseWindow;
    [SerializeField] private RectTransform resultWindow;
    [SerializeField] private Slider fuelBar;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image musicButtonImage;
    [SerializeField] private Image fuelBarImage;
    [Space(15)]
    [SerializeField] private Sprite toggleON;
    [SerializeField] private Sprite toggleOFF;

    [SerializeField] private Gradient fuelGradient;

    public float horizontalInput;
    private float curentFuelAmount;


    private void Awake()
    {
        if(InstanceGC == null) { InstanceGC = this; }
    }

    public void Start()
    {
        OutSmoothTransition();
        LoadSoundsSettings();
        LoadMusicSettings();       
    }

    public void UpdateFuelBarOnStart(float vechicleFuelValue)
    {
        fuelBar.maxValue = vechicleFuelValue;
        fuelBar.value = vechicleFuelValue;
    }

    public void UpdateFuelBar(float fuelAmount)
    {
        fuelBar.value = fuelAmount;
        fuelBarImage.color = fuelGradient.Evaluate(fuelBar.normalizedValue);

        if(fuelAmount == 0)
        {
            Invoke("OpenLoseWindow", 2f);
        }
    }

    private void OutSmoothTransition()
    {
        CanvasGroup[] disapearGroup = new CanvasGroup[] { smoothTransitionPanel };
        fadeController.Disappear(disapearGroup);
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

    public void CoinsCollect()
    {
        int newNumber = int.Parse(coinscounter.text);
        newNumber += 5;
        coinscounter.text = newNumber.ToString();
    }

    public IEnumerator OpenResultWindow(string reason)
    {
        yield return new WaitForSeconds(2.75f);
        moveController.MoveIn(resultWindow);

        if(reason == "Crash")
        {
            crashWariantCrash.enabled = true;
            crashWariant2Fuel.enabled = false;
        }
        else if(reason == "Fuel")
        {
            crashWariantCrash.enabled = false;
            crashWariant2Fuel.enabled = true;
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

    public void btn_Pause()
    {
        buttonSoundsPlayerAudioSource.Play();

        CanvasGroup[] groupToAppear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Appear(groupToAppear);
        moveController.MoveIn(pauseWindow);
    }
    public void btn_Resume()
    {
        buttonSoundsPlayerAudioSource.Play();

        CanvasGroup[] groupToDisappear = new CanvasGroup[] { fadeBackgroundPanel };
        fadeController.Disappear(groupToDisappear);
        moveController.MoveOut(pauseWindow);
    }
    public void btn_Home()
    {
        buttonSoundsPlayerAudioSource.Play();

        CanvasGroup[] appearGroup = new CanvasGroup[] { smoothTransitionPanel };
        fadeController.Appear(appearGroup);
        moveController.MoveOut(pauseWindow);
        StartCoroutine(LoadDelay("MainMenu"));
    }

    private IEnumerator LoadDelay(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

    #region MB_Controll
    public void OnBreakEnter() { horizontalInput = -1f; }
    public void OnBreakExit() { horizontalInput = 0f; }

    public void OnGasEnter() { horizontalInput = 1f; }
    public void OnGasExit() { horizontalInput = 0f; }
    #endregion
    #region PC_Controll
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
    #endregion
}