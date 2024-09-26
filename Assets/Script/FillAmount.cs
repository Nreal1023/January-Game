using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillAmount : MonoBehaviour
{
    public Image fillImage; // 이미지 채우기를 조절할 이미지
    public Image fillImage2; // 이미지 채우기를 조절할 이미지
    public Text countdownText; // 카운트 다운을 표시할 텍스트
    public GameObject backgroundObject; // 배경 오브젝트

    public float fillSpeed = 0.5f; // 이미지 채우기 속도 (높은 값일수록 빨리 채워짐)
    public float countdownDuration = 5.0f; // 카운트 다운 기간 (초)
    public float backgroundScaleY = 2.3f; // 배경 스케일 Y값
    public float finalBackgroundScaleY = 0.5f; // 최종 배경 스케일 Y값

    private bool isFillingImage1 = false;
    private bool isFillingImage2 = false;
    private bool isCountdownStarted = false;
    private float countdownTimer = 0.0f;

    private void Update()
    {
        // W 키를 처음 누르면 이미지1을 채웁니다.
        if (Input.GetKeyDown(KeyCode.W) && !isFillingImage1)
        {
            isFillingImage1 = true;
            StartCoroutine(FillImageAmount(fillImage, () => isFillingImage1 = false));
        }

        // 화살표 키를 처음 누르면 이미지2를 채웁니다.
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isFillingImage2)
        {
            isFillingImage2 = true;
            StartCoroutine(FillImageAmount(fillImage2, () => isFillingImage2 = false));
        }

        // 두 이미지 모두 채우는 중이고 카운트 다운이 시작되지 않았을 때 처리
        if (isFillingImage1 && isFillingImage2 && !isCountdownStarted)
        {
            // 배경 스케일 Y값을 변경
            Vector3 backgroundScale = backgroundObject.transform.localScale;
            backgroundScale.y = backgroundScaleY;
            backgroundObject.transform.localScale = backgroundScale;

            // 카운트 다운 시작
            isCountdownStarted = true;
            StartCoroutine(StartCountdown());
        }

        // 카운트 다운 중일 때 처리
        if (isCountdownStarted)
        {
            // 카운트 다운 텍스트 업데이트
            countdownTimer += Time.deltaTime;
            if (countdownTimer >= countdownDuration)
            {
                countdownText.text = "0";
                countdownTimer = 5.0f;

                // 최종 배경 스케일 Y값 설정
                Vector3 finalBackgroundScale = backgroundObject.transform.localScale;
                finalBackgroundScale.y = finalBackgroundScaleY;
                backgroundObject.transform.localScale = finalBackgroundScale;

                // fillImage, fillImage2 이미지 불투명도를 페이드 아웃
                StartCoroutine(FadeOutImages());

                // 다시 초기화
                isCountdownStarted = false;
                isFillingImage1 = false;
                isFillingImage2 = false;
            }
            else
            {
                int countdownValue = Mathf.CeilToInt(countdownDuration - countdownTimer);
                countdownText.text = countdownValue.ToString();
            }
        }
    }

    private IEnumerator FillImageAmount(Image imageToFill, System.Action onFillComplete)
    {
        float fillAmount = 0.5f;
        while (fillAmount < 1.0f)
        {
            fillAmount += fillSpeed * Time.deltaTime;
            imageToFill.fillAmount = fillAmount;
            yield return null;
        }

        // 채우기가 완료되면 상태를 리셋하고 콜백 함수를 호출합니다.
        onFillComplete?.Invoke();
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1.0f); // 1초 대기
        countdownText.text = Mathf.CeilToInt(countdownDuration).ToString();
    }

    private IEnumerator FadeOutImages()
    {
        float fadeDuration = 1.0f; // 페이드 아웃 지속 시간 (초)
        float elapsedTime = 0.0f;
        Color initialColor = fillImage.color;
        Color initialColor2 = fillImage2.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            fillImage.color = Color.Lerp(initialColor, Color.clear, t);
            fillImage2.color = Color.Lerp(initialColor2, Color.clear, t);
            yield return null;
        }

        // 이미지 불투명도를 완전히 0으로 설정
        fillImage.color = Color.clear;
        fillImage2.color = Color.clear;
    }
}



