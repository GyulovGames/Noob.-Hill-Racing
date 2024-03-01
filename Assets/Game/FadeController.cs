using System.Collections;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] private float fadeDuration;


    public void Appear( CanvasGroup[] appearCanvasGroup)
    {
        foreach (CanvasGroup group in appearCanvasGroup) 
        {
            group.gameObject.SetActive(true);
        }

        StartCoroutine(AppearCoroutine(appearCanvasGroup));      
    }

    public void Disappear(CanvasGroup[] disappearCanvasGroup)
    {
        StartCoroutine(DisappearCoroutine(disappearCanvasGroup));
    }



    public IEnumerator AppearCoroutine( CanvasGroup[] appdearCanvasGroup)
    { 
        float[] appearGroupAlpha = new float[appdearCanvasGroup.Length];
        float timePassed = 0f;

        for (int i = 0;  i < appdearCanvasGroup.Length; i++)
        {
            appearGroupAlpha[i] = appdearCanvasGroup[i].alpha;
        }

        while (timePassed < fadeDuration)
        {
            float normalizedTime = timePassed / fadeDuration;

            for(int i = 0;i < appdearCanvasGroup.Length;i++)
            {
                appdearCanvasGroup[i].alpha = Mathf.Lerp(appearGroupAlpha[i], 1f, normalizedTime);
            }

            yield return null;
            timePassed += Time.deltaTime;
        }

        foreach (CanvasGroup group in appdearCanvasGroup)
        {
            group.alpha = 1f;
        }
    }

    public IEnumerator DisappearCoroutine(CanvasGroup[] disappearCanvasGroup)
    {
        float[] appearGroupAlpha = new float[disappearCanvasGroup.Length];
        float timePassed = 0f;

        for (int i = 0; i < disappearCanvasGroup.Length; i++)
        {
            appearGroupAlpha[i] = disappearCanvasGroup[i].alpha;
        }

        while (timePassed < fadeDuration)
        {
            float normalizedTime = timePassed / fadeDuration;

            for (int i = 0; i < disappearCanvasGroup.Length; i++)
            {
                disappearCanvasGroup[i].alpha = Mathf.Lerp(appearGroupAlpha[i], 0f, normalizedTime);
            }

            yield return null;
            timePassed += Time.deltaTime;
        }

        foreach (CanvasGroup group in disappearCanvasGroup)
        {
            group.alpha = 0f;
            group.gameObject.SetActive(false);
        }
    }
}