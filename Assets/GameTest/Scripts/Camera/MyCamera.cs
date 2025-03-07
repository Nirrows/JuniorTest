using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public float fadeDuration = 2f;
    [SerializeField] private Image fadeImage;

    private bool _isFadeOut;
    public bool IsFadeOut() { return _isFadeOut; }

    public void FadeToBlack()
    {
        StartCoroutine(CR_FadeOut());
    }

    public void FadeFromBlack(float duration = 0)
    {
        StartCoroutine(CR_FadeIn(duration));
    }

    private IEnumerator CR_FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        _isFadeOut = true;
    }

    private IEnumerator CR_FadeIn(float duration = 0)
    {
        float elapsedTime = duration == 0 ? fadeDuration : duration;

        while (elapsedTime > 0f)
        {
            elapsedTime -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, elapsedTime / duration);
            yield return null;
        }

        _isFadeOut = false;
    }
}
