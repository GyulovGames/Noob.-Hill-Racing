using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EndlessRoadGenerator : MonoBehaviour
{
    public SpriteShapeController spriteShapeController;
    public Transform player;
    public float roadSegmentLength = 10f;
    public int maxSegments = 10;

    private List<Vector3> roadSegments = new List<Vector3>();
    private Vector3 lastPlayerPos;

    void Start()
    {
        lastPlayerPos = player.position;

        // Generate initial road segments
        for (int i = 0; i < maxSegments; i++)
        {
            GenerateRoadSegment();
        }
    }

    void Update()
    {
        float playerDistance = Vector3.Distance(player.position, lastPlayerPos);

        // Generate new road segments when player moves a certain distance
        if (playerDistance >= roadSegmentLength)
        {
            GenerateRoadSegment();
            RemoveOldRoadSegment();
            lastPlayerPos = player.position;
        }
    }

    void GenerateRoadSegment()
    {
        var lastRoadSegment = roadSegments.Count > 0 ? roadSegments[roadSegments.Count - 1] : Vector3.zero;
        Vector3 newRoadSegment = lastRoadSegment + Vector3.right * roadSegmentLength;

        spriteShapeController.spline.InsertPointAt(0, newRoadSegment);
        spriteShapeController.BakeCollider();

        roadSegments.Add(newRoadSegment);
    }

    void RemoveOldRoadSegment()
    {
        if (roadSegments.Count > maxSegments)
        {
            Vector3 oldRoadSegment = roadSegments[0];

            spriteShapeController.spline.RemovePointAt(spriteShapeController.spline.GetPointCount() - 1);
            spriteShapeController.BakeCollider();

            roadSegments.RemoveAt(0);
        }
    }
}