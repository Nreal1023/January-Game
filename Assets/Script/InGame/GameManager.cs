using System.Collections;
using UnityEngine;
using DG.Tweening;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject redBox;
    public GameObject blueBox;
    public Transform redBoxSpawnPosition;
    public Transform blueBoxSpawnPosition;
    public GameObject rectangleObject;
    public Text scoreText;

    public Text matchCountText; 
    public Button increaseButton;
    public Button decreaseButton;

    public Text winnerText; 
    public Text loserText;  
    public Text extraRoundText;

    public Button restartButton; // 게임 재시작 버튼
    public Button nextSceneButton; // 다음 씬으로 이동 버튼
    public string nextSceneName; // 다음 씬의 이름

    private Image restartButtonImage;
    private Image nextSceneButtonImage;

    private int redScore = 0;
    private int blueScore = 0;
    private int requiredWins;

    private void Start()
    {
        scoreText.gameObject.SetActive(false);

        requiredWins = int.Parse(matchCountText.text);
        increaseButton.onClick.AddListener(IncreaseMatchCount);
        decreaseButton.onClick.AddListener(DecreaseMatchCount);

        restartButton.gameObject.SetActive(false);
        nextSceneButton.gameObject.SetActive(false);

        restartButtonImage = restartButton.GetComponent<Image>();
        nextSceneButtonImage = nextSceneButton.GetComponent<Image>();

        restartButtonImage.enabled = false;
        nextSceneButtonImage.enabled = false;
    }

    private void Update()
    {
        if (!redBox.activeSelf || !blueBox.activeSelf)
        {
            ToggleRedBoxComponents(false);
            ToggleBlueBoxComponents(false);

            GameObject inactiveBox = redBox.activeSelf ? blueBox : redBox;
            inactiveBox.SetActive(true);
            ResetPlayer(inactiveBox);

            SpriteRenderer sr = inactiveBox.GetComponent<SpriteRenderer>();
            sr.DOFade(1, 1f);

            Transform spawnPosition = inactiveBox.CompareTag("RedBox") ? redBoxSpawnPosition : blueBoxSpawnPosition;
            inactiveBox.transform.DOMove(spawnPosition.position, 1f).OnComplete(() => {
                inactiveBox.transform.DOScale(Vector3.one, 1f);
            });

            rectangleObject.transform.DOScaleY(2.3f, 1f);

            if (inactiveBox.CompareTag("RedBox"))
            {
                blueScore++;
            }
            else
            {
                redScore++;
            }

            scoreText.text = $"{redScore} : {blueScore}";
            scoreText.gameObject.SetActive(true); 
            
            StartCoroutine(HideScoreUIAndPrepareNextRound());
        }
    }

    private void ResetPlayer(GameObject player)
    {
        ToggleComponent<ArrowKeyPlayerMovement>(player, true);
        ToggleComponent<WASDPlayerMovement>(player, true);
        ToggleComponent<BlueRazer>(player, true);
        ToggleComponent<RedRazer>(player, true);
    }

    private void ToggleComponent<T>(GameObject obj, bool isActive) where T : MonoBehaviour
    {
        T component = obj.GetComponent<T>();
        if (component)
        {
            component.enabled = isActive;
        }
    }

    private IEnumerator HideScoreUIAndPrepareNextRound()
    {
        yield return new WaitForSeconds(2f);

        scoreText.gameObject.SetActive(false);
        rectangleObject.transform.DOScaleY(0.3f, 1f);

        ToggleRedBoxComponents(true);
        ToggleBlueBoxComponents(true);

        if (redScore == requiredWins || blueScore == requiredWins)
        {
            if (redScore == blueScore)
            {
                ShowExtraRoundText();
                ResetPlayersHealth();
                requiredWins++;
            }
            else
            {
                EndGame();
            }
        }
        else
        {
            ResetPlayersHealth();
        }
    }

    public void IncreaseMatchCount()
    {
        requiredWins++;
        matchCountText.text = requiredWins.ToString();
    }

    public void DecreaseMatchCount()
    {
        if (requiredWins > 1)
        {
            requiredWins--;
            matchCountText.text = requiredWins.ToString();
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over");
        rectangleObject.transform.DOScaleY(11f, 1f);

        if (redScore > blueScore)
        {
            winnerText.text = "승리\n <color=#FF7777>빨강</color>";
            loserText.text = "패배\n <color=#81C1FF>파랑</color>";
        }
        else
        {
            winnerText.text = "승리\n <color=#81C1FF>파랑</color>";
            loserText.text = "패배\n <color=#FF7777>빨강</color>";
        }

        winnerText.gameObject.SetActive(true);
        loserText.gameObject.SetActive(true);

        ShowEndGameButtons();
    }

    void ShowEndGameButtons()
    {
        restartButton.gameObject.SetActive(true);
        nextSceneButton.gameObject.SetActive(true);

        restartButton.onClick.AddListener(ReloadCurrentScene);
        nextSceneButton.onClick.AddListener(GoToNextScene);

        restartButtonImage.enabled = true;
        nextSceneButtonImage.enabled = true;
    }

    void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GoToNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not specified!");
        }
    }

    void ShowExtraRoundText()
    {
        extraRoundText.text = "추가 라운드";
        extraRoundText.gameObject.SetActive(true);
        StartCoroutine(HideExtraRoundTextAfterDelay(2f));
    }

    IEnumerator HideExtraRoundTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        extraRoundText.gameObject.SetActive(false);
    }

    private void ToggleRedBoxComponents(bool isActive)
    {
        ToggleComponent<RedRazer>(redBox, isActive);
    }

    private void ToggleBlueBoxComponents(bool isActive)
    {
        ToggleComponent<BlueRazer>(blueBox, isActive);
    }

    private void ResetPlayersHealth()
    {
        redBox.GetComponent<Player>().ResetHealth();
        blueBox.GetComponent<Player>().ResetHealth();
    }
}
