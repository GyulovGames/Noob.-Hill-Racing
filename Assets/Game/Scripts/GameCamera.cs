using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private float folowSpeed;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform vehicleTransfor;


    private void FixedUpdate()
    {
        Vector3 desiredPosition = vehicleTransfor.position + cameraOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, folowSpeed);
        transform.position = smoothPosition;
    }
}
