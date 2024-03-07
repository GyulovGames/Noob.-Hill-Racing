using System;
using UnityEngine;
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
    [SerializeField] private CanvasGroup fadeBack1;
    [SerializeField] private CanvasGroup fadeBack2;
    [Space(5)]
    [SerializeField] private AudioSource buttonPlayer;
    [SerializeField] private Text coinsIndicator;
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


    private void Start()
    {
        DownloadData();
        UpdateCoinsIndicator();
        SpawnSelectedCar(LastSelectedCar);
        UpdateUpgradeSliders(LastSelectedCar);
    }


    private void DownloadData()
    {
        FreeCars = YandexGame.savesData.FreeCaras_sdk;
        LastSelectedCar = YandexGame.savesData.LastSelectedCar_sdk;
        Coins = YandexGame.savesData.Coins_sdk;
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
        }
    }
    private void SpawnSelectedCar(int carIndex)
    {
        GameObject lastCar = carsOnScene[LastSelectedCar];
        lastCar.SetActive(false);

        GameObject newCar = carsOnScene[carIndex];
        newCar.transform.position = new Vector2(0, 2f);
        newCar.SetActive(true);
        LastSelectedCar = carIndex;
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


    public void btn_UpgradeButtons(int partIndex)
    {
        switch(partIndex)
        {
            case 0:

                switch (LastSelectedCar)
                {
                    case 0:
                        upgradeSliders[partIndex].value += 1;
                        YandexGame.savesData.Car0_Upgrades[partIndex] += 1;
                        YandexGame.SaveProgress();
                        UpdateUpgradeSliders(LastSelectedCar);
                        break;
                }

                break;
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
           // LastSelectedCar = index;
            SpawnSelectedCar(index);
        }
        else if( Coins >= GetPurcahsePrice(index))
        {
            fadeController.FadeIn(fadeBack1);
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







    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //YandexGame.savesData.CarUpgrade_sdk[0, 0] = 5;

            YandexGame.savesData. Car0_Upgrades[0] = 10;
            YandexGame.savesData.Car0_Upgrades[1] = 5;
            YandexGame.savesData.Car0_Upgrades[2] = 3;
            YandexGame.savesData.Car0_Upgrades[3] = 4;
            YandexGame.savesData.Car0_Upgrades[4] = 5;




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