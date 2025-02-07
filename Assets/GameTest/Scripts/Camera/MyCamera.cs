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

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToBlack()
    {
        StartCoroutine(FadeOut());
    }

    public void FadeFromBlack()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
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

    private IEnumerator FadeIn()
    {
        float elapsedTime = fadeDuration;
        while (elapsedTime > 0f)
        {
            elapsedTime -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        _isFadeOut = false;
    }
}
