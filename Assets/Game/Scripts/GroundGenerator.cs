using UnityEngine;
using YG;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField] private float disableDistance;
    [SerializeField] private float partLength;
    [SerializeField] private Transform[] roadParts;

    private Transform carTransform;

    private void Start()
    {
        int selectedCar = YandexGame.savesData.LastSelectedCar_sdk;
        VehicleArray vehicleArray = GameObject.FindGameObjectWithTag("VehicleArray").GetComponent<VehicleArray>();
        carTransform = vehicleArray.vehicleTransform[selectedCar].transform;
    }

    private void Update()
    {
        float disablePosition = carTransform.position.x + disableDistance;

        for (int i = 0; i < roadParts.Length; i++)
        {
            if (roadParts[i].position.x < disablePosition)
            {
                Vector2 newPosition = new Vector2(partLength * roadParts.Length + roadParts[i].position.x, 0);
                roadParts[i].gameObject.SetActive(false);
                roadParts[i].position = newPosition;
            }

            float distanceToNearPart = Vector3.Distance(carTransform.position, roadParts[i].position);

            if (distanceToNearPart < 500)
            {
                roadParts[i].gameObject.SetActive(true);
            }
        }
    }
}