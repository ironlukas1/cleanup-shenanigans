using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        panel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestart);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerDied += () => Show("GAME OVER");
            GameManager.Instance.OnNightSurvived += () => Show("NIGHT SURVIVED");
        }
    }

    private void Show(string title)
    {
        panel.SetActive(true);
        titleText.text = title;

        if (statsText != null && GameManager.Instance != null)
            statsText.text = $"Kills: {GameManager.Instance.KillCount}";
    }

    private void OnRestart()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RestartGame();
    }
}
