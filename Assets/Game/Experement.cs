using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Experement : MonoBehaviour
{
    [SerializeField] private SpriteShapeController spriteShapeController;

    [SerializeField][Range(1f, 10f)] private int xMultyplayer;
    [SerializeField][Range(1f, 10f)] private int yMultyplayer;

    [SerializeField][Range(3f, 100f)] private int LevelLength;
    [SerializeField] private float noiseStep;

    [SerializeField] private float Height;

    private Vector2 lastPos;

    private void OnValidate()
    {
        for (int i = 0; i < LevelLength; i++)
        {
            lastPos = transform.position + new Vector3(i + xMultyplayer, Mathf.PerlinNoise(0, i * noiseStep) * yMultyplayer);
            spriteShapeController.spline.InsertPointAt(i, lastPos);

        }
    }
}