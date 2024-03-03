using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class RoadGenerator : MonoBehaviour
{
    public GameObject roadPrefab;
    public SpriteShapeController spriteShapeController;

    private List<GameObject> roadSegments = new List<GameObject>();

    void Start()
    {
        GenerateRoadSegment();
    }

    void Update()
    {
        if (roadSegments.Count == 0 || roadSegments[roadSegments.Count - 1].transform.position.x < 10f)
        {
            GenerateRoadSegment();
        }

        if (roadSegments.Count > 0 && roadSegments[0].transform.position.x < -10f)
        {
            Destroy(roadSegments[0]);
            roadSegments.RemoveAt(0);
            spriteShapeController.spline.SetPosition(0, roadSegments[0].transform.position);
        }
    }

    void GenerateRoadSegment()
    {
        GameObject newRoadSegment = Instantiate(roadPrefab, Vector3.zero, Quaternion.identity);
        roadSegments.Add(newRoadSegment);

        if (roadSegments.Count == 1)
        {
            spriteShapeController.spline.SetPosition(0, newRoadSegment.transform.position);
        }
        else
        {
            spriteShapeController.spline.InsertPointAt(1, newRoadSegment.transform.position);
        }
    }

}
