using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Up_Panel : MonoBehaviour
{
    public RectTransform panel;
    public Button openButton;
    public Button closeButton;
    public StartScreenManager startScreenManager;

    private void Start()
    {
        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void OpenPanel()
    {
        panel.DOAnchorPosY(0, 1.0f).SetEase(Ease.OutCubic);
        startScreenManager.DisableWAndIKeys();
    }

    private void ClosePanel()
    {
        float panelHeight = panel.rect.height;
        panel.DOAnchorPosY(-panelHeight, 1.0f).SetEase(Ease.InCubic);
        startScreenManager.EnableWAndIKeys();
    }
}


