using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Transform carTransform;

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, carTransform.position);

        if(distance > 50)
        {
            transform.position = new Vector2(carTransform.position.x - 20, 0);
        }
    }
}