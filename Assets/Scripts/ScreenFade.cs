using UnityEngine;
using System.Collections;
using Yarn.Unity;
public class ScreenFade : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration;

    [SerializeField] bool fadeIn;
    void Start()
    {
        if (fadeIn == true)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    [YarnCommand("fade_in_camera")]
    public void FadeIn()
    {
        StartCoroutine(CanvasFade(canvasGroup, canvasGroup.alpha, 1));
    }

    [YarnCommand("fade_out_camera")]
    public void FadeOut()
    {
        StartCoroutine(CanvasFade(canvasGroup, canvasGroup.alpha, 0));
    }

    private IEnumerator CanvasFade(CanvasGroup canvasGroup, float start, float end)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(start, end, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}
