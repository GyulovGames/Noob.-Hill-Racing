using System.Collections;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] private float fadeDuration;


    public void Appear( CanvasGroup canvasGroup)
    {
        StartCoroutine(AppearCoroutine(canvasGroup));
        canvasGroup.gameObject.SetActive(true);
    }

    public void Disappear(CanvasGroup canvasGroup)
    {
        StartCoroutine(DisappearCoroutine(canvasGroup));
    }



    public IEnumerator AppearCoroutine( CanvasGroup canvasGroup)
    {
        float appearGroupAlpha = canvasGroup.alpha;
        float timePassed = 0f;

        while (timePassed < fadeDuration)
        {
            float normalizedTime = timePassed / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(appearGroupAlpha, 1f, normalizedTime);

            yield return null;
            timePassed += Time.deltaTime;
        }

        canvasGroup.alpha = 1f;
    }

    public IEnumerator DisappearCoroutine(CanvasGroup canvasGroup)
    {
        float disappearGroupAlpha = canvasGroup.alpha;      
        float timePassed = 0f;

        while (timePassed < fadeDuration)
        {
            float normalizedTime = timePassed / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(disappearGroupAlpha, 0f, normalizedTime);

            yield return null;
            timePassed += Time.deltaTime;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }
}