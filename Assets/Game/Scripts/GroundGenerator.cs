using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField] private float ofset;
    [SerializeField] private Transform carTransform;
    [SerializeField] private Transform[] roadParts;
    [SerializeField] private float toPart;

    private void Update()
    {
        for(int i = 0; i < roadParts.Length; i++)
        {
            if (roadParts[i].position.x + toPart < carTransform.position.x)
            {
                // enable i
                Vector2 newPosition;
                newPosition.x = roadParts.Length * ofset + roadParts[i].position.x;
                newPosition.y = 0f;

                roadParts[i].position = new Vector2(newPosition.x, newPosition.y);
            }
        }
    }
}