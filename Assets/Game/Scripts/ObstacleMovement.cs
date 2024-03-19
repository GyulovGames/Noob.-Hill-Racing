using UnityEngine;
using YG;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float obstacleOffset;
    [SerializeField] private float stalkingDistance;
 
    private Transform carTransform;
    private Transform thisTransform;

    private void Start()
    {
        int selectedCar = YandexGame.savesData.LastSelectedCar_sdk;
        VehicleArray vehicleArray = GameObject.FindGameObjectWithTag("VehicleArray").GetComponent<VehicleArray>();
        carTransform = vehicleArray.vehicleTransform[selectedCar].transform;

        thisTransform = GetComponent<Transform>();
    }

    void Update()
    {
        float distance = Vector2.Distance(thisTransform.position, carTransform.position);

        if (distance > stalkingDistance)
        {
            thisTransform.position = new Vector2(carTransform.position.x - obstacleOffset, 0);
        }
    }
}