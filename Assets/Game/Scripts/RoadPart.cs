using UnityEngine;

public class RoadPart : MonoBehaviour
{
    void OnEnable()
    {
        EnableAllChildren(transform);
    }

    void EnableAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(true);
        }
    }
}