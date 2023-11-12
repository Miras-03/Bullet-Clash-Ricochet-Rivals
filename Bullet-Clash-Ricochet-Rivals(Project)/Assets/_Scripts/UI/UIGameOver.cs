using System;
using UISpace;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver
{
    private ImageSingleton imageSingleton;
    private TextMeshProSingleton textMeshProSingleton;
    private Crosshair crosshair;

    public UIGameOver()
    {
        imageSingleton = ImageSingleton.Instance;
        textMeshProSingleton = TextMeshProSingleton.Instance;
        crosshair = Crosshair.Instance;
    }

    public void SetColorAndMessage(Color color, string message)
    {
        DisableUIImages();
        DisableUITexts();
        SetColorAndEnableGameOverPanel(color);
        MessageText(message);
    }

    private void DisableUIImages()
    {
        imageSingleton.AmmoAmountInCircle.enabled = false;
        imageSingleton.AmmoBGInCircle.enabled = false;
        imageSingleton.HealthAmount.enabled = false;
        imageSingleton.HealthBG.enabled = false;

        crosshair.DisableCrosshairs();
    }

    private void DisableUITexts()
    {
        textMeshProSingleton.AmmoQuantityInText.enabled = false;
        textMeshProSingleton.MagQuantityInText.enabled = false;
    }

    private void SetColorAndEnableGameOverPanel(Color color)
    {
        imageSingleton.GameOverPanel.enabled = true;
        imageSingleton.SetGameOverPanelColor(color);
    }

    private void MessageText(string message)
    {
        textMeshProSingleton.GameOverMessage.enabled = true;
        textMeshProSingleton.GameOverMessage.text = message;
    }
}
