using UnityEngine;
using YG;

public class VehicleArray : MonoBehaviour
{
    public Transform[] vehicleTransform;

    private void Start()
    {
        int selectedCar = YandexGame.savesData.LastSelectedCar_sdk;
        vehicleTransform[selectedCar].gameObject.SetActive(true);
    }
}