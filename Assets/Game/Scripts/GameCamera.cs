using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private float folowSpeed;
    [SerializeField] private Vector3 cameraOffset;

    private Transform cameraTransform;
    private Transform carTransform;

    private void Start()
    {
        int selectedCar = YandexGame.savesData.LastSelectedCar_sdk;
        VehicleArray vehicleArray = GameObject.FindGameObjectWithTag("VehicleArray").GetComponent<VehicleArray>();
        carTransform = vehicleArray.vehicleTransform[selectedCar].transform;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = carTransform.position + cameraOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, folowSpeed);
        transform.position = smoothPosition;
    }
}