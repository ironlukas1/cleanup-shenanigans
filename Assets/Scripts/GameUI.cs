using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI modeText;

    [Header("Player Reference")]
    [SerializeField] private PlayerWeaponManager weaponManager;

    private void Update()
    {
        if (GameManager.Instance == null) return;

        // Timer
        float t = GameManager.Instance.TimeRemaining;
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        timerText.text = $"{minutes}:{seconds:00}";

        // Kills
        killText.text = $"Kills: {GameManager.Instance.KillCount}";

        // Ammo & mode
        if (weaponManager != null)
        {
            ammoText.text = $"Shells: {weaponManager.CurrentAmmo}";
            modeText.text = weaponManager.CurrentMode == WeaponMode.Mop ? "MOP" : "SHOTGUN";
        }
    }
}
