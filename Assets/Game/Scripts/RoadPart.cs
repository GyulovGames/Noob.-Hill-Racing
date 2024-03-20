using UnityEngine;
using YG;

public class RoadPart : MonoBehaviour
{
    void OnEnable()
    {
        EnableAllChildren(transform);
    }

    void EnableAllChildren(Transform parent)
    {
        if (!YandexGame.savesData.Sounds_sdk)
        {
            foreach (Transform child in parent)
            {
                AudioSource audio = child.GetComponent<AudioSource>();
                audio.volume = 0f;
                child.gameObject.SetActive(true);              
            }
        }
        else
        {
            foreach (Transform child in parent)
            {
                AudioSource audio = child.GetComponent<AudioSource>();
                audio.volume = 1f;
                child.gameObject.SetActive(true);
            }
        }
    }
}