using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartScreenManager : MonoBehaviour
{
    public Image leftReadyImage;
    public Image rightReadyImage;
    public Text countdownText;
    public GameObject your3DObject;

    public GameObject WASDPlayerObject;
    public GameObject ArrowKeyPlayerObject;
    public GameObject RedRazer;
    public GameObject BlueRazer;

    private bool isLeftReady = false;
    private bool isRightReady = false;
    private bool isCountingDown = false;
    private bool isGameStarted = false;
    private bool canPressWAndI = true;

    public Image settingButton;
    public Image ExitButton;
    
    void Update()
    {
        if (isCountingDown || isGameStarted || !canPressWAndI) return;

        if (Input.GetKeyDown(KeyCode.W) && !isLeftReady)
        {
            leftReadyImage.DOFillAmount(1f, 0.5f).OnComplete(() => 
            {
                isLeftReady = true;
            });
        }

        if (Input.GetKeyDown(KeyCode.I) && !isRightReady)
        {
            rightReadyImage.DOFillAmount(1f, 0.5f).OnComplete(() => 
            {
                isRightReady = true;
            });
        }

        if (isLeftReady && isRightReady)
        {
            StartCoroutine(StartCountdown());
            isLeftReady = false;
            isRightReady = false;
        }
    }

    IEnumerator StartCountdown()
    {
        settingButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        isCountingDown = true;
        leftReadyImage.transform.parent.gameObject.SetActive(false);
        rightReadyImage.transform.parent.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        TweenYScale(your3DObject.transform, 2.5f, 1f).OnComplete(() =>
        {
            StartCoroutine(CountdownRoutine());
        });
    }

    Tween TweenYScale(Transform targetTransform, float targetYScale, float duration)
    {
        return targetTransform.DOScaleY(targetYScale, duration).SetEase(Ease.OutQuad);
    }

    IEnumerator CountdownRoutine()
    {
        for (int i = 3; i >= 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        StartGame();
    }

    void StartGame()
    {
        isCountingDown = false;
        isGameStarted = true;
        countdownText.text = "";

        TweenYScale(your3DObject.transform, 0.3f, 1f).OnComplete(() => 
        {
            WASDPlayerObject.GetComponent<WASDPlayerMovement>().enabled = true;
            ArrowKeyPlayerObject.GetComponent<ArrowKeyPlayerMovement>().enabled = true;
            
            WASDPlayerObject.GetComponent<BlueRazer>().enabled = true;
            ArrowKeyPlayerObject.GetComponent<RedRazer>().enabled = true;
        });
    }

    public void EnableWAndIKeys()
    {
        canPressWAndI = true;
    }

    public void DisableWAndIKeys()
    {
        canPressWAndI = false;
    }
}
