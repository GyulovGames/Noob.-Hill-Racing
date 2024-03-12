using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField] private float disableDistance;
    [SerializeField] private float partLength;
    [SerializeField] private float obstacleOffset;
    [SerializeField] private float stalkingDistance;
    [SerializeField] private Transform carTransform;
    [SerializeField] private Transform[] roadParts;
    [SerializeField] private Transform obltacleTransform;

    private void Update()
    {
        RoadGenerate();
        ObstacleMovement();
    }

    private void RoadGenerate()
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
    private void ObstacleMovement()
    {
        float distance = Vector2.Distance(obltacleTransform.position, carTransform.position);

        if (distance > stalkingDistance)
        {
            obltacleTransform.position = new Vector2(carTransform.position.x - obstacleOffset, 0);
        }
    }
}