using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Playing, Won, Lost }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float nightDuration = 255f; // 4 min 15 sec
    public float TimeRemaining { get; private set; }
    public int KillCount { get; private set; }
    public GameState State { get; private set; } = GameState.Playing;

    public event System.Action OnPlayerDied;
    public event System.Action OnNightSurvived;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        TimeRemaining = nightDuration;
    }

    private void Update()
    {
        if (State != GameState.Playing) return;

        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining <= 0f)
        {
            TimeRemaining = 0f;
            NightComplete();
        }
    }

    public void RegisterKill()
    {
        KillCount++;
    }

    public void PlayerDied()
    {
        if (State != GameState.Playing) return;
        State = GameState.Lost;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnPlayerDied?.Invoke();
    }

    private void NightComplete()
    {
        if (State != GameState.Playing) return;
        State = GameState.Won;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnNightSurvived?.Invoke();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
