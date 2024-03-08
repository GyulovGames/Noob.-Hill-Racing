using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private float folowSpeed;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform carSpawnPoint;
    [SerializeField] private GameObject[] CarsArray;

    private void Start()
    {
        int carIndex = YandexGame.savesData.LastSelectedCar_sdk;

        for (int i = 0; i < CarsArray.Length; i++)
        {
            if (i == carIndex)
            {
                cameraTarget = CarsArray[i].transform;

                Transform carTransform = CarsArray[carIndex].transform;
                carTransform.position = carSpawnPoint.position;
                carTransform.gameObject.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = cameraTarget.position + cameraOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, folowSpeed);
        transform.position = smoothPosition;
    }
}
