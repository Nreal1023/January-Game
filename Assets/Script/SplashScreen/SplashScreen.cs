using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class SplashScreen : MonoBehaviour
{
    public Image image;
    public Text text;

    public float imageFadeInDuration = 1.0f;
    public float imageDisplayDuration = 2.0f;
    public float imageFadeOutDuration = 1.0f;
    
    public float textFadeInDuration = 1.0f;
    public float textDisplayDuration = 2.0f;
    public float textFadeOutDuration = 1.0f;
    public float nextSceneDelay = 2.5f;
    
    public string nextSceneName;

    private void Start()
    {
        image.DOFade(1, imageFadeInDuration).OnComplete(() =>
        {
            StartCoroutine(WaitAndFadeOutImage());
        });
    }

    IEnumerator WaitAndFadeOutImage()
    {
        yield return new WaitForSeconds(imageDisplayDuration);
        image.DOFade(0, imageFadeOutDuration).OnComplete(() =>
        {
            text.DOFade(1, textFadeInDuration).OnComplete(() =>
            {
                StartCoroutine(WaitAndFadeOutText());
            });
        });
    }

    IEnumerator WaitAndFadeOutText()
    {
        yield return new WaitForSeconds(textDisplayDuration);
        text.DOFade(0, textFadeOutDuration).OnComplete(() =>
        {
            StartCoroutine(WaitAndLoadNextScene());
        });
    }

    IEnumerator WaitAndLoadNextScene()
    {
        yield return new WaitForSeconds(nextSceneDelay);
        SceneManager.LoadScene(nextSceneName);
    }
}





