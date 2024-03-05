using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]

public class TrailGenerator : MonoBehaviour
{
    [SerializeField] private SpriteShapeController spriteShapeController;

    [SerializeField][Range(3f, 100f)] private int levelLenght = 30;
    [SerializeField][Range(1f, 50f)] private float xMultiplier = 2f;
    [SerializeField][Range(1f, 50f)] private float yMultiplier = 2f;
    [SerializeField][Range(0f, 1f)] private float curveSmoothness = 0.5f;

    [SerializeField] private float noiseStep = 0.5f;
    [SerializeField] private float botton = 10f;

    private Vector3 lastPoint;

    private void OnValidate()
    {
        spriteShapeController.spline.Clear();

        for (int i = 0; i < levelLenght; i++)
        {
            lastPoint = transform.position + new Vector3(i * xMultiplier, Mathf.PerlinNoise(0, i * noiseStep) * yMultiplier);
            spriteShapeController.spline.InsertPointAt(i, lastPoint);


            if (i != 0 && i != levelLenght - 1)
            {
                spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                spriteShapeController.spline.SetLeftTangent(i, Vector3.left * xMultiplier * curveSmoothness);
                spriteShapeController.spline.SetRightTangent(i, Vector3.right * xMultiplier * curveSmoothness);
            }
        }

        spriteShapeController.spline.InsertPointAt(levelLenght, new Vector3(lastPoint.x, transform.position.y - botton));
        spriteShapeController.spline.InsertPointAt(levelLenght + 1, new Vector3(transform.position.x, transform.position.y - botton));
    }
}